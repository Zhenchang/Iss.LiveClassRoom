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
    public class VideosController : BaseController
    {
        private IVideoService _service;
        private IUserService _userService;

        public VideosController(IVideoService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        public ActionResult Index(int? status)
        {
            new Video().CheckAuthorization(Permissions.List);
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
            var model = new Video();
            model.CheckAuthorization(Permissions.Create);
            PopulateDropDownLists();
            return View("Edit", model.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VideoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var domainModel = new Video();
                domainModel.CheckAuthorization(Permissions.Create);
                domainModel.Title = viewModel.Title;
                domainModel.FileName = viewModel.FileName;
                // Get the course
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

            PopulateDropDownLists();
            return View(entity.ToViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VideoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var domainModel = await _service.GetById(viewModel.Id);

                domainModel.CheckAuthorization(Permissions.Edit);

                domainModel.FileName = viewModel.FileName;
                domainModel.Title = viewModel.Title;
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

        protected void PopulateDropDownLists(string selectedId = null)
        {
            if (string.IsNullOrEmpty(selectedId))
            {
                ViewBag.InstructorId = new SelectList(_userService.GetAll().OfType<Instructor>(), "Id", "Name");
            }
            else
            {
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