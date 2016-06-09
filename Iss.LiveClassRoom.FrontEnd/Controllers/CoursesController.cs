using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.FrontEnd.Models;
using Iss.LiveClassRoom.FrontEnd.App_Start;
using System.ComponentModel.DataAnnotations;
using Iss.LiveClassRoom.ServiceLayer;
using Iss.LiveClassRoom.DataAccessLayer;

namespace Iss.LiveClassRoom.FrontEnd.Controllers
{
    [Authorize]
    public class CoursesController : BaseController
    {

        private ICourseService _service;
        private IUserService _userService;
        private IStudentService _studentService;
        public CoursesController(ICourseService service, IUserService userService, 
            IStudentService studentService)
        {
            _service = service;
            _userService = userService;
            _studentService = studentService;
        }

        public ActionResult Index(int? status)
        {
            new Course().CheckAuthorization(Permissions.List);
            RenderStatusAlert(status);
            return View(_service.GetAll());
        }

        public async Task<ActionResult> Details(string id, int? status)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            entity.CheckAuthorization(Permissions.View);

            RenderStatusAlert(status);
            ViewBag.InstructorName = entity.Instructor.Name;
            var viewModel = entity.ToViewModel();
            if (User.IsInRole("Student"))
            {
                var student = await _studentService.GetById(User.Identity.Name);
                viewModel.Quizzes = entity.GetUnAnsweredQuizzesFor(student);
            }
            IService<EnrollmentRequest> enrollService = new Service<EnrollmentRequest>(new UnitOfWork(new SystemContext()));
            ICollection<EnrollRequestViewModel> requests = new List<EnrollRequestViewModel>();
            foreach (var item in enrollService.GetAll().Where(n => n.CourseId.Equals(id)).AsEnumerable())
            {
                EnrollRequestViewModel requestViewModel = new EnrollRequestViewModel(item);
                Student student = _studentService.GetById(item.StudentId).Result;
                requestViewModel.Student = student.ToViewModel();
                requests.Add(requestViewModel);
            }
            ViewBag.enrollRequests = requests;
            return View(viewModel);
        }

        public async Task<ActionResult> AssignStudent(string id)
        {
            var model = await _service.GetById(id);

            model.CheckAuthorization(Permissions.Link);

            ViewBag.Course = model;
            var remainingStudents = _studentService.GetAll().ToList().Except(model.Students);
            return View("AssignStudents", remainingStudents.ToList());
        }
        [HttpPost]
        public async Task<ActionResult> AssignStudents(string[] studentIds, string id)
        {  
            if(studentIds == null) RedirectToAction("Details", new { id = id });
            var course = await _service.GetById(id);

            course.CheckAuthorization(Permissions.Link);

            foreach (var studentId in studentIds)
            {
                var student = await _studentService.GetById(studentId);
                //course.Students.Add(student);
                await _service.AssignStudent(student, course, GetLoggedInUserId());
                course.Instructor.ToString();
                await _service.Update(course, GetLoggedInUserId());
            }
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<ActionResult> DeassignStudent(string id, string studentId)
        {
            var student = await _studentService.GetById(studentId);
            var course = await _service.GetById(id);
            course.Students.Remove(student);
            course.CurrentStudentNumber--;
            course.Instructor.ToString();
            await _service.Update(course, GetLoggedInUserId());
            //return new HttpStatusCodeResult(HttpStatusCode.OK);
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult Create()
        {
            var model = new Course();
            model.CheckAuthorization(Permissions.Create);
            PopulateDropDownLists();
            return View("Edit", model.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseViewModel viewModel)
        {
            Instructor instructor = null;
            try {
                instructor = (await _userService.GetById(viewModel.InstructorId)) as Instructor;
                if (instructor == null) throw new ValidationException("No instructor found!");
            }
            catch (ValidationException ex) {
                ModelState.AddModelError("InstructorId", ex);
            }
            if (ModelState.IsValid)
            {
                var domainModel = viewModel.ToDomainModel();
                domainModel.CheckAuthorization(Permissions.Create);
                domainModel.Name = viewModel.Name;
                domainModel.Instructor = instructor;
                await _service.Add(domainModel, GetLoggedInUserId());
                return RedirectToAction("Index", new { status = 0 });
            }
            PopulateDropDownLists(viewModel.InstructorId);
            return View("Edit", viewModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            entity.CheckAuthorization(Permissions.Edit);

            PopulateDropDownLists();
            return View(entity.ToViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CourseViewModel viewModel)
        {

            Instructor instructor = null;
            try {
                instructor = (await _userService.GetById(viewModel.InstructorId)) as Instructor;
                if (instructor == null) throw new ValidationException("No instructor found!");
            }
            catch (ValidationException ex) {
                ModelState.AddModelError("InstructorId", ex);
            }

            if (ModelState.IsValid) {

                var domainModel = await _service.GetById(viewModel.Id);

                domainModel.CheckAuthorization(Permissions.Edit);

                domainModel.Name = viewModel.Name;
                domainModel.MaxStudentNumber = viewModel.MaxStudentNumber;
                if (domainModel.Instructor.Id != viewModel.InstructorId) {
                    domainModel.Instructor = instructor;
                }
                await _service.Update(domainModel, GetLoggedInUserId());
                return RedirectToAction("Details", new { id = domainModel.Id, status = 1 });

            }
            PopulateDropDownLists(viewModel.InstructorId);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<HttpStatusCodeResult> ConfirmDelete(string id) {
            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();
            entity.CheckAuthorization(Permissions.Delete);
            try {
                await _service.Remove(entity, GetLoggedInUserId());
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex) {
                LogException(ex);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        protected void PopulateDropDownLists(string selectedId = null) {
            if (string.IsNullOrEmpty(selectedId)) {
                ViewBag.InstructorId = new SelectList(_userService.GetAll().OfType<Instructor>(), "Id", "Name");
            }
            else {
                ViewBag.InstructorId = new SelectList(_userService.GetAll().OfType<Instructor>(), "Id", "Name", selectedId);
            }
        }

        public async Task<ActionResult> Register(string sortOrder)
        {
            //ViewBag.CourseSortParam = String.IsNullOrEmpty(sortOrder) ? "course_desc" : "";
            Student student = await _studentService.GetById(GetLoggedInUserId());
            IEnumerable<Course> courses =  _service.GetAll().Where(n => !n.Students.Any(m => m.Id.Equals(student.Id))).AsEnumerable();
            return View(courses); 
        }

        public ActionResult RegisterCourse(string courseId)
        {   
            //invoke workflow
            EnrollStudentServiceClient client = new EnrollStudentServiceClient();
            client.RecvEnrollmentReq(GetLoggedInUserId(), courseId);
            return RedirectToAction("", "Account");
        }

        public ActionResult Accept(string studentId, string courseId)
        {
            //invoke workflow
            EnrollStudentServiceClient client = new EnrollStudentServiceClient();
            client.EnrollDecision(true, studentId, courseId);
            return Redirect("/Courses/Details/" + courseId + "#enrollments");
        }

        public ActionResult Reject(string studentId, string courseId)
        {
            //invoke workflow
            EnrollStudentServiceClient client = new EnrollStudentServiceClient();
            client.EnrollDecision(false, studentId, courseId);
            return Redirect("/Courses/Details/" + courseId + "#enrollments");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
