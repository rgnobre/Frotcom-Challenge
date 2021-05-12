using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Frotcom.Challenge.Data.Models;
using Frotcom.Challenge.SendTrackingDataWorker.Services.IServices;

namespace Frotcom.Challenge.SendTrackingDataWorker.LocalStorage
{
    public class PacketLocalStorage : IPacketLocalStorage
    {
        private readonly IPublishService<Packet> _publishService;


        private const int _maxTrakingPackets = 100;

        private int _totalPacket = 0;
        public int TotalPacket  // read-only instance property
        {
            get => _totalPacket;
        }

        private int _totalPacketPortugal = 0;

        public int TotalPacketPortugal
        {
            get => _totalPacketPortugal;
        }

        public ConcurrentDictionary<int, List<Packet>> LocalStoragePackets { get; set; } = new ConcurrentDictionary<int, List<Packet>>();
        private object _lock = new object();

        public PacketLocalStorage(IPublishService<Packet> publishService)
        {
            _publishService = publishService;
        }


        public void AddPacket(Packet packet)
        {
            lock (_lock)
            {
                try
                {
                    Interlocked.Increment(ref _totalPacketPortugal);

                    var packets = new List<Packet>();
                    packets.Add(packet);

                    LocalStoragePackets.AddOrUpdate(packet.VehicleId, packets, (key, existListPacket) =>
                    {
                        if (existListPacket.Count <= _maxTrakingPackets)
                            existListPacket.Add(packet);
                        else
                            Interlocked.Decrement(ref _totalPacketPortugal);

                        return existListPacket;
                    });

                    if (LocalStoragePackets[packet.VehicleId].Count == _maxTrakingPackets)
                    {
                        _publishService.Post(LocalStoragePackets[packet.VehicleId]);
                        LocalStoragePackets.TryRemove(packet.VehicleId, out List<Packet> list);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void IncrementTotalPacket()
        {
            Interlocked.Increment(ref _totalPacket);
        }
    }
}
