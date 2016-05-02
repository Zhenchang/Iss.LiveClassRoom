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
    [Authorize]
    public class UsersController : BaseController
    {
        private IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        public ActionResult Index(int? status, string type)
        {
            RenderStatusAlert(status);

            var list = _service.GetAll();
            if (type == "Instructor") {
                new Instructor().CheckAuthorization(Permissions.List);
                list = list.OfType<Instructor>();
                ViewBag.Title = "Instructors List";
            }
            else if(type == "Student") {
                new Student().CheckAuthorization(Permissions.List);
                list = list.OfType<Student>();
                ViewBag.Title = "Students List";
            }
            return View(list);
        }

        public async Task<ActionResult> Details(string id, int? status)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            entity.CheckAuthorization(Permissions.View);

            RenderStatusAlert(status);
            if(entity is Instructor) {
                return View((entity as Instructor).ToViewModel());
            }
            else if(entity is Student) {
                return View((entity as Student).ToViewModel());
            }
            else {
                return HttpNotFound();
            }
        }

        public ActionResult Create(string type)
        {
            User model = null;           
            if (type == "Instructor") {
                model = new Instructor();
                model.CheckAuthorization(Permissions.Create);
                ViewBag.IsInstructor = true;
                return View("Edit", (model as Instructor).ToViewModel());
            }
            else if (type == "Student") {
                model = new Student();
                model.CheckAuthorization(Permissions.Create);
                ViewBag.IsInstructor = false;
                return View("Edit", (model as Student).ToViewModel());
            }else {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InstructorViewModel viewModel, string type)
        {
            User domainModel = null;
            if ("Instructor".Equals(type)) {
                domainModel = new Instructor() { IsAdmin = viewModel.IsAdmin };
                ViewBag.IsInstructor = true;
            }
            else if ("Student".Equals(type)) {
                domainModel = new Student();
                ViewBag.IsInstructor = false;
            }
            if (ModelState.IsValid)
            {
                domainModel.PasswordHash = _service.HashPassword("123456");
                domainModel.Name = viewModel.Name;
                domainModel.Email = viewModel.Email;
                domainModel.PhoneNumber = viewModel.PhoneNumber;

                domainModel.CheckAuthorization(Permissions.Create);
                try {
                    await _service.Add(domainModel, GetLoggedInUserId());
                } catch(Exception ex) {
                    ex.Message.ToString();
                }
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

            if (entity is Instructor) {
                ViewBag.IsInstructor = true;
                return View((entity as Instructor).ToViewModel());
            } else if (entity is Student) {
                ViewBag.IsInstructor = false;
                return View((entity as Student).ToViewModel());
            } else {
                return HttpNotFound();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InstructorViewModel viewModel)
        {
            if (ModelState.IsValid) {
                var domainModel = await _service.GetById(viewModel.Id);

                domainModel.CheckAuthorization(Permissions.Edit);

                domainModel.Name = viewModel.Name;
                domainModel.Email = viewModel.Email;
                domainModel.PhoneNumber = viewModel.PhoneNumber;
                if (domainModel is Instructor) {
                    (domainModel as Instructor).IsAdmin = viewModel.IsAdmin;
                }
                await _service.Update(domainModel, GetLoggedInUserId());
                return RedirectToAction("Details", new { id = domainModel.Id, status = 1 });

            }
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
