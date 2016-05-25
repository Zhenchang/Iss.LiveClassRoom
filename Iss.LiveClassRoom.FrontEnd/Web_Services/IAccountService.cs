using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Web_Services
{   
    [ServiceContract]
    public interface IAccountService
    {   
        [OperationContract]
        String login(String name, String password);
    }
}
