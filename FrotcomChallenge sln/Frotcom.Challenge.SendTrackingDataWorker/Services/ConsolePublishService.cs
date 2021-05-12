using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Frotcom.Challenge.Data.Models;
using Frotcom.Challenge.SendTrackingDataWorker.Services.IServices;

namespace Frotcom.Challenge.SendTrackingDataWorker.Services
{
    class ConsolePublishService : IPublishService<Packet>
    {
        public Task Post(List<Packet> obj)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("pt-PT"))} : Vehicle {obj[0].VehicleId} sent {obj.Count} packets in Portugal");
            });
        }
    }
}
