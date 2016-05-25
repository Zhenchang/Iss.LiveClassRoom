using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.FrontEnd.Web_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Iss.LiveClassRoom.ServiceLayer.Web_Services
{
    class AccountService : IAccountService
    {
        private IUserService _service;
        private static JavaScriptSerializer json;

        public AccountService(IUserService service)
        {
            _service = service;
            json = new JavaScriptSerializer();
    }

        public Cookie login(string name, string password)
        {
            var user = _service.ValidateUserInfo(name, password);
            if (user == null)
                return null;
            var userInfo = new UserInfo()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
            if (user is Instructor)
            {
                userInfo.Roles.Add("Instructor");
                if ((user as Instructor).IsAdmin) userInfo.Roles.Add("Admin");
            }
            else if (user is Student)
            {
                userInfo.Roles.Add("Student");
            }
            var userData = json.Serialize(userInfo);
            var ticket = new FormsAuthenticationTicket(1, user.Id, DateTime.Now, DateTime.Now.AddMinutes(20), false, userData);
            var encTicket = FormsAuthentication.Encrypt(ticket);
            Cookie cookie = new Cookie();
            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Content = encTicket;
            
            return cookie;
        }

        string IAccountService.login(string name, string password)
        {
            throw new NotImplementedException();
        }

        private class UserInfo
        {

            public string Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public List<string> Roles { get; set; } = new List<string>();
        }
    }
}
