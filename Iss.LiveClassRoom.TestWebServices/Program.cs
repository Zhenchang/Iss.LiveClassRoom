using Iss.LiveClassRoom.WebService.DataContracts;
using Iss.LiveClassRoom.WebService.ServiceContracts;
using Iss.LiveClassRoom.WebService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.TestWebServices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start client");
            ChannelFactory<ICourseWService> myCF = new ChannelFactory<ICourseWService>("CourseServiceClient");
            Console.WriteLine("Please input UserName:");
            string user = Console.ReadLine();
            Console.WriteLine("Please input Password:");
            string pwd = Console.ReadLine();
            Console.WriteLine(user + ": " + pwd);
            myCF.Credentials.UserName.UserName = user;
            myCF.Credentials.UserName.Password = pwd;
            ICourseWService client = myCF.CreateChannel();
            ICollection<CourseData> courses = client.GetCoursesByStudentEmail();
            Console.WriteLine(courses.Count());
            Console.WriteLine(courses.ToString() + "result");
            Console.WriteLine("Input anything to quit...");
            Console.Read();

            if (myCF != null && myCF.State != CommunicationState.Closed)
                myCF.Close();

        }
    }
}
