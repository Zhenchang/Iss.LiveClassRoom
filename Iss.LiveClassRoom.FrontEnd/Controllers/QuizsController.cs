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
        private IUserService _userService;

        public QuizsController(IQuizService service, ICourseService courseService, IUserService userService)
        {
            _service = service;
            _courseService = courseService;
            _userService = userService;
        }

        public ActionResult Index(int? status)
        {
            new Quiz().CheckAuthorization(Permissions.List);
            RenderStatusAlert(status);
            return View(_service.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> Answer(string id, string answer) {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            var student = await _userService.GetById(GetLoggedInUserId());
            if (student is Student) {
                
                var option = entity.Options.SingleOrDefault(x => x.Id == answer);
                if (option != null) {
                    option.StudentAnswers.Add(new StudentAnswer()
                    {
                        Time = DateTime.UtcNow,
                        Student = student as Student,
                        QuizOption = option
                    });
                    try {
                        entity.Course.ToString();
                        option.Quiz.ToString();
                        await _service.Update(entity, GetLoggedInUserId());
                    }
                    catch(Exception ex) {
                        ex.Message.ToString();
                    }
              
                    return RedirectToAction("Index", "Account");
                }
            }
            return RedirectToAction("Details", new { id = entity.Id, type = "Answer", status = 4 });
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
                domainModel.CheckAuthorization(Permissions.Create);
                domainModel.Question = viewModel.Question;
                domainModel.StartDate = viewModel.StartDate;
                domainModel.EndDate = viewModel.EndDate;
                domainModel.Course = course;
                foreach(var options in viewModel.Options) {
                    domainModel.Options.Add(new QuizOption()
                    {
                         Quiz = domainModel,
                         Text = options.Text
                    });
                }
                await _service.Add(domainModel, GetLoggedInUserId());
                return RedirectToAction("Index", new { status = 0 });
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
                return RedirectToAction("Details", new { id = domainModel.Id, status = 1 });
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
