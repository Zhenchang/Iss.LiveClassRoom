using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.FrontEnd.App_Start;
using Iss.LiveClassRoom.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Iss.LiveClassRoom.FrontEnd.Controllers
{
    public class AccountController : BaseController
    {
        private IUserService _service;

        public AccountController(IUserService service) {
            _service = service;
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var user = await _service.GetById(User.Identity.Name);
            if (user == null) throw new AuthorizationException();

            if (User.IsInRole("Student")) {
                return View("StudentIndex", (user as Student).ToViewModel());
            }
            if (User.IsInRole("Instructor")) {
                return View("InstructorIndex", (user as Instructor).ToViewModel());
            }
            throw new AuthorizationException();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult LogOff() {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                var user = _service.ValidateUserInfo(model.Email, model.Password);
                if (user == null) return View(model);

                var result = SignInManager.SignIn(user);
                if (!result) return View(model);

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}