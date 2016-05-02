using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Services
{
    public interface IUserService : IService<User>
    {
        User ValidateUserInfo(string email, string password);
        Task ChangePassword(string email, string oldPassword, string newPassword, string byUserId);
        string HashPassword(string password);
    }
}
