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
using Iss.LiveClassRoom.FrontEnd.App_Start;
using Iss.LiveClassRoom.FrontEnd.Models;
using System.ComponentModel.DataAnnotations;

namespace Iss.LiveClassRoom.FrontEnd.Controllers
{
    [Authorize]
    public class QuizsController : BaseController
    {
        private IQuizService _service;
        private ICourseService _courseService;
        private IStudentService _studentService;

        public QuizsController(IQuizService service, ICourseService courseService, IStudentService studentService)
        {
            _service = service;
            _courseService = courseService;
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<ActionResult> Answer(string id, string answer) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var quiz = await _service.GetById(id);
            if (quiz == null) return HttpNotFound();

            quiz.CheckAuthorization(Permissions.PartialEdit);

            var student = await _studentService.GetById(GetLoggedInUserId());
            if (student is Student) {
                
                var option = quiz.Options.SingleOrDefault(x => x.Id == answer);
                // The Student Can't Answer Again
                if (option != null && !student.HasAnsweredQuiz(quiz)) {
                    option.StudentAnswers.Add(new StudentAnswer()
                    {
                        Time = DateTime.UtcNow,
                        Student = student as Student,
                        QuizOption = option
                    });
                    try {
                        quiz.Course.ToString();
                        option.Quiz.ToString();
                        await _service.Update(quiz, GetLoggedInUserId());
                    }
                    catch(Exception ex) {
                        ex.Message.ToString();
                    }
              
                    return RedirectToAction("Index", "Account");
                }
            }
            return RedirectToAction("Details", new { id = quiz.Id, type = "Answer", status = 4 });
        }

        public async Task<ActionResult> Details(string id, string type, int? status)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            entity.CheckAuthorization(Permissions.View);

            RenderStatusAlert(status);
            ViewBag.CourseName = entity.Course.Name;
            if (type.Equals("Answer")) {
                return View("Answer", entity.ToViewModel());
            }
            else {
                return View(entity.ToViewModel());
            }
        }

        public async Task<ActionResult> Create(string id)
        {
            var course = await _courseService.GetById(id);
            if (course == null || !course.Instructor.Id.Equals(GetLoggedInUserId())) throw new AuthorizationException();

            var model = new Quiz();
            model.Course = course;
            model.CheckAuthorization(Permissions.Create);

            var viewModel = model.ToViewModel();
            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuizViewModel viewModel)
        {
            Course course = null;
            try
            {
                course = await _courseService.GetById(viewModel.Id);
                viewModel.CourseId = course.Id;
                if (course == null) throw new ValidationException("No course found!");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("CourseId", ex);
            }
            if (ModelState.IsValid)
            {
                var domainModel = new Quiz();
                domainModel.Course = course;
                domainModel.CheckAuthorization(Permissions.Create);
                domainModel.Question = viewModel.Question;
                domainModel.StartDate = viewModel.StartDate;
                domainModel.EndDate = viewModel.EndDate;
                foreach(var options in viewModel.Options) {
                    domainModel.Options.Add(new QuizOption()
                    {
                         Quiz = domainModel,
                         Text = options.Text
                    });
                }
                await _service.Add(domainModel, GetLoggedInUserId());
                return RedirectToAction("Details", new { id = domainModel.Id, status = 0, type="View" });
            }
            return View("Edit", viewModel);

        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            entity.CheckAuthorization(Permissions.Edit);

            return View(entity.ToViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QuizViewModel viewModel)
        {

            Course course = null;
            try
            {
                course = await _courseService.GetById(viewModel.CourseId);
                if (course == null) throw new ValidationException("No course found!");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("CourseId", ex);
            }

            if (ModelState.IsValid)
            {

                var domainModel = await _service.GetById(viewModel.Id);

                domainModel.CheckAuthorization(Permissions.Edit);

                domainModel.Question = viewModel.Question;
                domainModel.StartDate = viewModel.StartDate;
                domainModel.EndDate = viewModel.EndDate;
                domainModel.Course.ToString();

                await _service.Update(domainModel, GetLoggedInUserId());
                return RedirectToAction("Details", new { id = domainModel.Id, status = 1, type = "View" });
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<HttpStatusCodeResult> ConfirmDelete(string id)
        {
            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();
            entity.CheckAuthorization(Permissions.Delete);
            try
            {
                await _service.Remove(entity, GetLoggedInUserId());
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
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
