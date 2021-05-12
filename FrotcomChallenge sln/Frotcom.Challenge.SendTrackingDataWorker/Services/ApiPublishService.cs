using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Frotcom.Challenge.Data.Models;
using Frotcom.Challenge.SendTrackingDataWorker.Services.IServices;

namespace Frotcom.Challenge.SendTrackingDataWorker.Services
{
   public class ApiPublishService : IPublishService<Packet>
    {
        public Task Post(List<Packet> obj)
        {
            throw new NotImplementedException();
        }
    }
}
