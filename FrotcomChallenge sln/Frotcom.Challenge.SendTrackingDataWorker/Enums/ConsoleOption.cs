using System.ComponentModel;

namespace Frotcom.Challenge.SendTrackingDataWorker.Enums
{
    public enum ConsoleOptions
    {
        [Description("1 - RUN")] Run = 1,
        [Description("2 - STOP")] Stop = 2,
        [Description("0 - EXIT")] Exit = 0
    }
}
