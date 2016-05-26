using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Iss.LiveClassRoom.WebService.DataContracts
{   
    [DataContract]
    public class CourseData
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Instructor { get; set; }

        public CourseData(string id, string name, string instructor)
        {
            Id = id;
            Name = name;
            Instructor = instructor;

        }
    }
}