using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Web_Services
{   
    [DataContract]
    public class Cookie
    {   
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Content { get; set; }
    }
}