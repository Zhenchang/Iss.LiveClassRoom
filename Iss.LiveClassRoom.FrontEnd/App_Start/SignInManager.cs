using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Iss.LiveClassRoom.FrontEnd.App_Start {


    public static class IdentityExtensions {
        public static string GetGivenName(this IIdentity identity) {
            if (identity == null)
                return null;

            return (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.GivenName);
        }

        public static string GetEmail(this IIdentity identity) {
            if (identity == null)
                return null;

            return (identity as ClaimsIdentity).FirstOrNull(ClaimTypes.Email);
        }

        internal static string FirstOrNull(this ClaimsIdentity identity, string claimType) {
            var val = identity.FindFirst(claimType);

            return val == null ? null : val.Value;
        }
    }

    public static class SignInManager {

        private static JavaScriptSerializer json = new JavaScriptSerializer();

        public static bool ReSignIn() {
            var ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            if (ticket == null) return false;
            if (ticket.Expired) return false;
            
            var userInfo = DeSerializeUserInfo(ticket.UserData);
            if (userInfo == null) return false;

            var identity = new GenericIdentity(userInfo.Id, "FormsAuthentication");
            identity.AddClaim(new Claim(ClaimTypes.GivenName, userInfo.Name));
            identity.AddClaim(new Claim(ClaimTypes.Email, userInfo.Email));
            var principal = new GenericPrincipal(identity, userInfo.Roles.ToArray());
            HttpContext.Current.User = principal;
            return true;
        }

        public static bool SignIn(User user) {
            var userInfo = new UserInfo()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
            if (user is Instructor) {
                userInfo.Roles.Add("Instructor");
                if((user as Instructor).IsAdmin) userInfo.Roles.Add("Admin");
            }else if (user is Student) {
                userInfo.Roles.Add("Student");
            }
            var userData = SerializeUserInfo(userInfo);
            var ticket = new FormsAuthenticationTicket(1, user.Id, DateTime.Now, DateTime.Now.AddMinutes(20), false, userData);
            var encTicket  = FormsAuthentication.Encrypt(ticket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return true;
        }

        public static void SignOut() {
            FormsAuthentication.SignOut();
        }


        private class UserInfo {

            public string Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public List<string> Roles { get; set; } = new List<string>();
        }

        private static string SerializeUserInfo(UserInfo user) {
            return json.Serialize(user);
        }

        private static UserInfo DeSerializeUserInfo(string userInfo) {
            return json.Deserialize<UserInfo>(userInfo);
        }
    }
}