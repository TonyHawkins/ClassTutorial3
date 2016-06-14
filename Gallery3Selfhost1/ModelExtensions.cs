using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallery3Selfhost1.DTO;

namespace Gallery3Selfhost1
{
    public abstract partial class Work
    {
        public clsWork MapToDTO()
        {
            clsWork lcWorkDTO = GetDTO();
            lcWorkDTO.Name = Name;
            lcWorkDTO.Date = Date;
            lcWorkDTO.Value = Value;
            lcWorkDTO.ArtistName = ArtistName;
            return lcWorkDTO;
        }

        protected abstract clsWork GetDTO();
    }


    public partial class Painting
    {
        protected override clsWork GetDTO()
        {
            return new clsPainting()
            { Width = this.Width, Height = this.Height, Type = this.Type };
        }
    }
    public partial class Photograph
    {
        protected override clsWork GetDTO()
        {
            return new clsPhotograph()
            { Width = this.Width, Height = this.Height, Type = this.Type };
        }
    }
    public partial class Sculpture
    {
        protected override clsWork GetDTO()
        {
            return new clsSculpture()
            { Weight = this.Weight, Material = this.Material };
        }
    }
}
