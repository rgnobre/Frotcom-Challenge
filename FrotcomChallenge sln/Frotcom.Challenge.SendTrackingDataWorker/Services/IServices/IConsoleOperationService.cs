using System;
using Frotcom.Challenge.SendTrackingDataWorker.Commons;

namespace Frotcom.Challenge.SendTrackingDataWorker.Services.IServices
{
    public interface IConsoleOperationService
    {
        event EventHandler<StatusEventArgs> UpdateStatus;
        void Run();
        void Stop();
        void Exit();
    }
}
