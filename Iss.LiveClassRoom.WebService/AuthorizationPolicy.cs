using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.DataAccessLayer;
using Iss.LiveClassRoom.ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;

namespace Iss.LiveClassRoom.WebService
{
    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        private string id;

        public AuthorizationPolicy()
        {
            id = Guid.NewGuid().ToString();
        }

        public string Id
        {
            get
            {
                return id;
            }
        }

        public ClaimSet Issuer
        {
            get
            {
                return ClaimSet.System;
            }
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            string email = string.Empty;
            foreach (ClaimSet claimSet in evaluationContext.ClaimSets)
            {
                foreach (System.IdentityModel.Claims.Claim claim in claimSet.FindClaims(System.IdentityModel.Claims.ClaimTypes.Name, Rights.PossessProperty))
                {
                    email = claim.Resource.ToString();
                }
            }
            IUserService userService = new UserService(new UnitOfWork(new SystemContext()));
            User user = userService.GetAll().SingleOrDefault(n => n.Email.Equals(email));
            var identity = new GenericIdentity(user.Name);
            identity.AddClaim(new System.Security.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Name, user.Name));
            identity.AddClaim(new System.Security.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Email, user.Email));
            List<string> roles = new List<string>();
            if (user is Instructor)
            {
                roles.Add("Instructor");
                if ((user as Instructor).IsAdmin)
                    roles.Add("Admin");
            }
            else if (user is Student)
                roles.Add("Student");
            var principal = new GenericPrincipal(identity, roles.ToArray());
            evaluationContext.Properties["Principal"] = principal;
            return true;
        }
    }
}