using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.FrontEnd.App_Start;
using Iss.LiveClassRoom.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Iss.LiveClassRoom.FrontEnd.Controllers
{
    [Authorize]
    public class TopicsController : BaseController
    {
        private ITopicService _service;
        private ICourseService _courseService;

        public TopicsController(ITopicService service, ICourseService courseService)
        {
            _service = service;
            _courseService = courseService;
        }

        public async Task<ActionResult> Details(string id, int? status)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            entity.CheckAuthorization(Permissions.View);

            RenderStatusAlert(status);

            return View(entity.ToViewModel());
        }

        public async Task<ActionResult> Create(string id)
        {
            var course = await _courseService.GetById(id);
            if (course == null || !course.Instructor.Id.Equals(GetLoggedInUserId())) throw new AuthorizationException();

            var model = new Topic();
            model.Course = course;
            model.CheckAuthorization(Permissions.Create);
            PopulateDropDownLists();
            return View("Edit", model.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TopicViewModel viewModel)
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
                var domainModel = new Topic();
                domainModel.Course = course;
                domainModel.CheckAuthorization(Permissions.Create);
                domainModel.Content = viewModel.Content;
                domainModel.Title = viewModel.Title;
                await _service.Add(domainModel, GetLoggedInUserId());
                return RedirectToAction("Index", new { status = 0 });
            }
            PopulateDropDownLists(viewModel.CourseId);
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
        public async Task<ActionResult> Edit(TopicViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var domainModel = await _service.GetById(viewModel.Id);

                domainModel.CheckAuthorization(Permissions.Edit);

                domainModel.Content = viewModel.Content;
                domainModel.Title = viewModel.Title;
                domainModel.Course.ToString();
                try
                {
                    await _service.Update(domainModel, GetLoggedInUserId());
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                }
              
                return RedirectToAction("Details", new { id = domainModel.Id, status = 1 });
            }
            PopulateDropDownLists(viewModel.CourseId);
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

        protected void PopulateDropDownLists(string selectedId = null)
        {
            if (string.IsNullOrEmpty(selectedId))
            {
                ViewBag.CourseId = new SelectList(_courseService.GetAll().Where(m => m.Instructor.Id == GetLoggedInUserId()), "Id", "Name");
            }
            else
            {
                ViewBag.CourseId = new SelectList(_courseService.GetAll().Where(m => m.Instructor.Id == GetLoggedInUserId()), "Id", "Name", selectedId);
            }
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