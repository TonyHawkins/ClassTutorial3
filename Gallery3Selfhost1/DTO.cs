
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

        public Artist MapToEntity()
        {
            return new Artist()
            { Name = this.Name, Phone = this.Phone, Speciality = this.Speciality };
        }
        [DataMember]
        public ICollection<clsWork> Works { get; set; }
    }
    [DataContract]
    [KnownType(typeof(clsPainting))]
    [KnownType(typeof(clsPhotograph))]
    [KnownType(typeof(clsSculpture))]
    public abstract partial class clsWork
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public System.DateTime Date { get; set; }
        [DataMember]
        public decimal Value { get; set; }
        [DataMember]
        public string ArtistName { get; set; }
        public Work MapToEntity()
        {
            Work lcWorkEnt = GetEntity();
            lcWorkEnt.Name = Name;
            lcWorkEnt.Date = Date;
            lcWorkEnt.Value = Value;
            lcWorkEnt.ArtistName = ArtistName;
            return lcWorkEnt;

        }
        protected abstract Work GetEntity();
    }


    public partial class clsPainting : clsWork
    {
        [DataMember]
        public Nullable<float> Width { get; set; }
        [DataMember]
        public Nullable<float> Height { get; set; }
        [DataMember]
        public string Type { get; set; }
        protected override Work GetEntity()
        {
            return new Painting()
            { Width = this.Width, Height = this.Height, Type = this.Type };
        }
    }

    public partial class clsPhotograph : clsWork
    {
        [DataMember]
        public Nullable<float> Width { get; set; }
        [DataMember]
        public Nullable<float> Height { get; set; }
        [DataMember]
        public string Type { get; set; }

        protected override Work GetEntity()
        {
            return new Photograph()
            { Width = this.Width, Height = this.Height, Type = this.Type };
        }
    }
    public partial class clsSculpture : clsWork
    {
        [DataMember]
        public Nullable<float> Weight { get; set; }
        [DataMember]
        public string Material { get; set; }
        protected override Work GetEntity()
        {
            return new Sculpture()
            {  Weight = this.Weight,  Material = this.Material };
        }
    }

}

