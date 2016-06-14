using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gallery3Selfhost1.DTO
{
   [DataContract]
    public class clsArtist
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Speciality { get; set; }
        [DataMember]
        public string Phone { get; set; }

        //[DataMember]
        //public ICollection<clsWork> Works {get;set;}
    }
}
