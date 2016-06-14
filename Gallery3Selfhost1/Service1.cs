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
        private int process<TEntity>(TEntity prItem, System.Data.Entity.EntityState prState) where TEntity : class
        {
            using (Gallery_DataEntities lcContext = new Gallery_DataEntities())
            {
                lcContext.Entry(prItem).State = prState;
                int lcCount = lcContext.SaveChanges();
                return lcCount;
            }
        }

        public int UpdateArtist(clsArtist prArtist)
        {
            return process(prArtist.MapToEntity(), System.Data.Entity.EntityState.Modified);
        }

        public int InsertArtist(clsArtist prArtist)
        {
            return process(prArtist.MapToEntity(), System.Data.Entity.EntityState.Added);
        }

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
