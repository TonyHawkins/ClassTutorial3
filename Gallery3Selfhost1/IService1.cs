using Gallery3Selfhost1.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Gallery3Selfhost1
{
    [ServiceContract]
    interface IService1
    {
        [OperationContract]
        List<string> GetArtistNames();
        [OperationContract]
        clsArtist GetArtist(string prArtistName);
    }
}
