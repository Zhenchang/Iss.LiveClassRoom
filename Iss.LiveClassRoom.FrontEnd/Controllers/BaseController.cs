using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;

namespace Iss.LiveClassRoom.FrontEnd.Controllers
{
    public class BaseController : Controller {


        protected const string Err_General = "There was an error occured while performing your last request. Please try again later.";
        protected static string[] StatusMessages = { "Create action was successful", "Edit action was successful", "Delete action was successful", "Last action was successful", "Last action failed!" };


        public void RenderStatusAlert(int? status) {
            if (status.HasValue) {
                ViewBag.StatusMessage = StatusMessages[status.Value];
                ViewBag.Status = "success";
                if (status.Value > 3) ViewBag.Status = "danger";
            }
        }
        protected string GetLoggedInUserId()
        {
            return User.Identity.Name;
        }

        protected void LogException(Exception ex) {

        }
    }
}