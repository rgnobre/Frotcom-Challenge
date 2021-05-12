using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Frotcom.Challenge.Queue;
using Frotcom.Challenge.SendTrackingDataWorker.Commons;
using Frotcom.Challenge.SendTrackingDataWorker.Enums;
using Frotcom.Challenge.SendTrackingDataWorker.LocalStorage;
using Frotcom.Challenge.SendTrackingDataWorker.Services.IServices;

namespace Frotcom.Challenge.SendTrackingDataWorker.Services
{
    public class ConsoleOperationService : IConsoleOperationService
    {
        public event EventHandler<StatusEventArgs> UpdateStatus;
        private readonly IQueueProcessorFactory _queueProcessorFactory;
        private readonly IPacketLocalStorage _localStorage;
        private QueueProcessorHost queueProcessorHost;
        private Timer logTimer;

        public ConsoleOperationService(IQueueProcessorFactory queueProcessorFactory, IPacketLocalStorage localStorage)
        {
            _queueProcessorFactory = queueProcessorFactory;
            _localStorage = localStorage;
        }


        public async void Run()
        {
            try
            {
                WriteLog();
                // Create a timer and set a ten second interval.
                logTimer = new Timer();
                logTimer.Interval = 10000;
                logTimer.Elapsed += OnTimedEvent;
                logTimer.AutoReset = true;
                logTimer.Start();

                queueProcessorHost = new QueueProcessorHost(_queueProcessorFactory, 100, 100);
                await queueProcessorHost.Run();
                OnUpdateAppStatus(new StatusEventArgs
                { Status = true, ChangeTime = DateTime.Now, Action = ConsoleOptions.Run });
            }
            catch (Exception)
            {
                Stop();
            }
        }

        public void Stop()
        {
            ProcessorHostDispose();
            Console.WriteLine("Stopped");
            OnUpdateAppStatus(new StatusEventArgs { Status = true, ChangeTime = DateTime.Now, Action = ConsoleOptions.Stop });
        }

        public void Exit()
        {
            ProcessorHostDispose();
            Console.WriteLine("EXIT");
            OnUpdateAppStatus(new StatusEventArgs { Status = false, ChangeTime = DateTime.Now, Action = ConsoleOptions.Exit });
        }


        private void ProcessorHostDispose()
        {
            logTimer?.Stop();
            logTimer = null;

            queueProcessorHost?.Stop();
            queueProcessorHost = null;
        }

        public virtual void OnUpdateAppStatus(StatusEventArgs e)
        {
            UpdateStatus?.Invoke(this, e);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            WriteLog();
        }

        private void WriteLog()
        {
            Console.WriteLine($"{DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("pt-PT"))}: Total: {_localStorage.TotalPacket}, InPortugal: {_localStorage.TotalPacketPortugal}");
        }
    }
}
