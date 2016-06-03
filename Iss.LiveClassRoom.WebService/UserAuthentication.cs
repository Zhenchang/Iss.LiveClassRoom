using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.DataAccessLayer;
using Iss.LiveClassRoom.ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Iss.LiveClassRoom.WebService
{
    public class UserAuthentication : UserNamePasswordValidator
    {
        public override void Validate(string email, string password)
        {
            IUserService userService = new UserService(new UnitOfWork(new SystemContext()));
            User user = userService.ValidateUserInfo(email, password);
            if (user == null)
            {
                throw new FaultException("Unknown Username or Incorrect Password");
            }
        }
    }
}