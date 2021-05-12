using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frotcom.Challenge.Data.Models;
using Frotcom.Challenge.Queue;
using Frotcom.Challenge.Reverse.Geocoding;
using Frotcom.Challenge.SendTrackingDataWorker.LocalStorage;

namespace Frotcom.Challenge.SendTrackingDataWorker.Classes
{
    public class QueueProcessor : IQueueProcessor
    {
        private readonly IPacketLocalStorage _localStorage;
        private ReverseGeocoding reverseGeocoding = new ReverseGeocoding();
        private static object _lock = new object();

        public QueueProcessor(IPacketLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task Process(IEnumerable<Packet> packets, CancellationToken cancellationToken)
        {
            var task = packets.Select(async item =>
            {
                _localStorage.IncrementTotalPacket();
                var country = await reverseGeocoding.GetCountry(item.Latitude, item.Longitude);

                if (country == Country.Portugal)
                {
                    _localStorage.AddPacket(item);
                }
            });

            await Task.WhenAny(task);
        }
    }
}
