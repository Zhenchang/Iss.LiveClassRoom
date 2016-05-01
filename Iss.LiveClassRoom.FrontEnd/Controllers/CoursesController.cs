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

namespace Iss.LiveClassRoom.FrontEnd.Controllers
{
    public class CoursesController : BaseController
    {

        private ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            return View(entity.ToViewModel());
        }

        public ActionResult Create()
        {
            return View("Edit", new Course().ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var domainModel = viewModel.ToDomainModel();
                domainModel.CreatedByUserId = "1";
                domainModel.TimeCreatedUtc = DateTime.UtcNow;
                try
                {
                    await _service.Add(domainModel, "###");
                    
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                
                return RedirectToAction("Details", new { id = domainModel.Id });
            }

            return View("Edit", viewModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var entity = await _service.GetById(id);
            if (entity == null) return HttpNotFound();

            return View(entity.ToViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var domainModel = await _service.GetById(viewModel.Id);

                domainModel.Name = viewModel.Name;

                //if (domainModel.Instructor.Id != viewModel.InstructorId) {
                //    // ToDo : ###
                //}

                await _service.Update(domainModel, "###");
                return RedirectToAction("Details", new { id = domainModel.Id });
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            var entity = await _service.GetById(id);
            await _service.Remove(entity, "###");
            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
