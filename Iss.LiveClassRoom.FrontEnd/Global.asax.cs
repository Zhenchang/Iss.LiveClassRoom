using Iss.LiveClassRoom.FrontEnd.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Iss.LiveClassRoom.FrontEnd
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }


        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e) {
            if (FormsAuthentication.CookiesSupported == true) {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null) {
                    try {
                        var result = SignInManager.ReSignIn();
                    }
                    catch { }
                }
            }
        }
    }
}
