using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Iss.LiveClassRoom.WebService.DataContracts
{   
    [DataContract]
    public class VideoData
    {   
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string FileName { get; set; }

        public VideoData(string id, string title, string fileName)
        {
            Id = id;
            Title = title;
            FileName = fileName;
        }
    }
}