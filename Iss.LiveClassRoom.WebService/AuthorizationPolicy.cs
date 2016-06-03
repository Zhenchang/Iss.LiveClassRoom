using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.WebService
{
    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        public string Id
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public ClaimSet Issuer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            throw new NotImplementedException();
        }
    }
}