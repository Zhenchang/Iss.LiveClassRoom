using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Iss.LiveClassRoom.WebService.DataContracts
{   
    [DataContract]
    public class QuizData
    {  
        [DataMember] 
        public string Id { get; set; }

        [DataMember]
        public string Question { get; set; }

        [DataMember]
        public string Course { get; set; }

        [DataMember]
        public ICollection<OptionData> Options { get; set; }

        [DataContract]
        public class OptionData
        {
            [DataMember]
            public string Id { get; set; }

            [DataMember]
            public string Text { get; set; }

            public OptionData(string id, string text)
            {
                Id = id;
                Text = text;
            }
        }
    }
}