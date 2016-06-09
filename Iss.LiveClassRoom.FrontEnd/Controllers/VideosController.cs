using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.FrontEnd.App_Start;
using Iss.LiveClassRoom.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
        private ICourseService _courseService;
        private IUserService _userService;

        public VideosController(IVideoService service, IUserService userService, ICourseService courseService)
        {
            _service = service;
            _userService = userService;
            _courseService = courseService;
        }

        public async Task<ActionResult> Details(string id, int? status)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            entity.CheckAuthorization(Permissions.View);

            RenderStatusAlert(status);
            ViewBag.FileName = entity.FileName;
            return View(entity.ToViewModel());
        }

        public async Task<ActionResult> Create(string id)
        {
            var model = new Video();
            var course = await _courseService.GetById(id);
            if (course == null || !course.Instructor.Id.Equals(GetLoggedInUserId())) throw new AuthorizationException();
            model.Course = course;
            model.CheckAuthorization(Permissions.Create);
            PopulateDropDownLists();
            return View("Edit", model.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VideoViewModel viewModel, HttpPostedFileBase videoFile)
        {
            var course = await _courseService.GetById(viewModel.Id);
            var allowedExt = new string[] { ".mp4", ".ogg", ".mpeg" };
            if (course == null || !course.Instructor.Id.Equals(GetLoggedInUserId())) throw new AuthorizationException();
            if (!allowedExt.Contains(Path.GetExtension(videoFile.FileName)))
            {
                ModelState.AddModelError("", Path.GetExtension(videoFile.FileName.ToUpper() + 
                    " is not an acceptable file type"));
                return View("Edit", viewModel);
            }
            if (ModelState.IsValid)
            {
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(videoFile.FileName);
                //var path = Path.Combine(Server.MapPath("~/Content/videos"), fileName);
                var path = Path.Combine(Server.MapPath("~/Content/videos"), fileName);
                videoFile.SaveAs(path);
                var domainModel = new Video();
                domainModel.Course = course;
                domainModel.CheckAuthorization(Permissions.Create);
                domainModel.Title = viewModel.Title;
                domainModel.FileName = fileName;
           
                // Get the course
                await _service.Add(domainModel, GetLoggedInUserId());

                UploadFile(domainModel.Id);

                return RedirectToAction("Details","Courses",new { id=course.Id});
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
                var file = Path.Combine(Server.MapPath("~/Content/Videos/"), entity.FileName);
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
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

        public void UploadFile(string videoId)
        {
            //invoke workflow
            fileuploadClient client = new fileuploadClient();
            client.ReceiveUploadMessage(GetLoggedInUserId(), videoId);
        }
    }
}