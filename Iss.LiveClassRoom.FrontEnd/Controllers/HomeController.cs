using Iss.LiveClassRoom.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Iss.LiveClassRoom.Core.Models;

namespace Iss.LiveClassRoom.FrontEnd.Controllers
{
    public class HomeController : BaseController
    {
        IUserService _service;

        public HomeController(IUserService userService)
        {
            _service = userService;
        }

        public ActionResult Index() {

            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error() {
            return View();
        }

        async public System.Threading.Tasks.Task<ActionResult> Install()
        {
            var User = new Instructor();
            User.Name = "admin";
            User.PasswordHash = "7c4a8d09ca3762af61e59520943dc26494f8941b";
            User.Email = "admin@iss.com";
            User.PhoneNumber = "8888888";
            User.IsAdmin = true;
            await _service.Add(User, "###");
            await _service.Update(User, "###");
            await _service.Update(User, "###");
            return RedirectToAction("Login", "Account");
        }
    }
}