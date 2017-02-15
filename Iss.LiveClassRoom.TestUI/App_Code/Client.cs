using Iss.LiveClassRoom.WebService.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.TestUI.App_Code
{
    public class Client
    {
        public static ICourseWService getClient(string email, string password)
        {
            ChannelFactory<ICourseWService> myCF = new ChannelFactory<ICourseWService>("CourseServiceClient");
            myCF.Credentials.UserName.UserName = email;
            myCF.Credentials.UserName.Password = password;
            return myCF.CreateChannel();
        }
    }
}
