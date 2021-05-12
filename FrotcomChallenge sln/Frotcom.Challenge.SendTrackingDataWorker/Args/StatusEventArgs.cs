using System;
using Frotcom.Challenge.SendTrackingDataWorker.Enums;

namespace Frotcom.Challenge.SendTrackingDataWorker.Commons
{
    public class StatusEventArgs : EventArgs
    {
        public bool Status{ get; set; }
        public DateTime ChangeTime { get; set; }
        public ConsoleOptions Action { get; set; }

    }

}
