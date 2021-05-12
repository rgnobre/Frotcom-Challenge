using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Frotcom.Challenge.Data.Models;
using Frotcom.Challenge.SendTrackingDataWorker.Args;

namespace Frotcom.Challenge.SendTrackingDataWorker.LocalStorage
{
    public interface IPacketLocalStorage
    {
        int TotalPacket { get; }
        int TotalPacketPortugal { get; }
        ConcurrentDictionary<int, List<Packet>> LocalStoragePackets { get; set; }
        void AddPacket(Packet packet);
        void IncrementTotalPacket();

    }
}
