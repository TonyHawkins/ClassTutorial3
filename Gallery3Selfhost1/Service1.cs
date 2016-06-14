using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Gallery3Selfhost1.DTO;

namespace Gallery3Selfhost1
{
    class Service1 : IService1
    {
        public List<string> GetArtistNames()
        {
            using (Gallery_DataEntities lcContext = new Gallery_DataEntities())
                return lcContext.Artists
                        .Select(lcArtist => lcArtist.Name)
                        .ToList();
        }
        public clsArtist GetArtist(string prArtistName)
        {
            using (Gallery_DataEntities lcContext = new Gallery_DataEntities())
            {
                Artist lcArtist = lcContext.Artists
                                    .Include("Works")
                                    .Where(Artist => Artist.Name == prArtistName)
                                    .FirstOrDefault();
                clsArtist lcArtistDTO = new clsArtist()
                { Name = lcArtist.Name, Speciality = lcArtist.Speciality, Phone = lcArtist.Phone };
                return lcArtistDTO;
            }
        }
    }
}
