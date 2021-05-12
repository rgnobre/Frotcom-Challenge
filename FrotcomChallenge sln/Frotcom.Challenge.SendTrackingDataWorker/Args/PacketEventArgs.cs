using System;
using System.Collections.Generic;
using Frotcom.Challenge.Data.Models;

namespace Frotcom.Challenge.SendTrackingDataWorker.Args
{
    public class PacketEventArgs : EventArgs
    {
        public bool Status { get; set; }
        public List<Packet> ListPackets { get; set; }

    }
}
