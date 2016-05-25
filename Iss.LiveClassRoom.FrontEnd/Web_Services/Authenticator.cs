using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Selectors;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.ServiceLayer.Services;
using Iss.LiveClassRoom.DataAccessLayer;
using System.IdentityModel.Tokens;

namespace Iss.LiveClassRoom.FrontEnd.Web_Services
{
    public class Authenticator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            IUserService _service = new UserService(new UnitOfWork(new SystemContext()));
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");
            var user = _service.ValidateUserInfo(userName, password);
            if(user == null)
                throw new SecurityTokenException("Unkown username or password.");
            Console.Write("Done: Credentials accepted. \n");
        }
    }
}