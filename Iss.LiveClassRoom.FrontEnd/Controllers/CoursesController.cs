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

namespace Iss.LiveClassRoom.FrontEnd.Controllers
{
    public class CoursesController : BaseController
    {

        private ICourseService _service;
        private IUserService _userService;

        public CoursesController(ICourseService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
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

            return View(entity.ToViewModel());
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
                await _service.Add(domainModel, User.Identity.Name);
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
                if (domainModel.Instructor.Id != viewModel.InstructorId) {
                    domainModel.Instructor = instructor;
                }
                await _service.Update(domainModel, User.Identity.Name);
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
                await _service.Remove(entity, User.Identity.Name);
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
