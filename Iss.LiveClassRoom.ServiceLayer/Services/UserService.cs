using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iss.LiveClassRoom.Core.Repositories;
using System.Security.Cryptography;

namespace Iss.LiveClassRoom.ServiceLayer.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task ChangePassword(string email, string oldPassword, string newPassword, string byUserId) {
            var user = ValidateUserInfo(email, oldPassword);
            user.PasswordHash = HashPassword(newPassword);
            await Update(user, byUserId);
        }

        public User ValidateUserInfo(string email, string password) {
            string hashedPassword = HashPassword(password);
            var user = GetAll().SingleOrDefault(x => x.Email.Equals(email) && x.PasswordHash.Equals(hashedPassword));
            return user;
        }

        public string HashPassword(string password) {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(password));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }
    }
}
