using Frotcom.Challenge.Queue;
using Frotcom.Challenge.SendTrackingDataWorker.LocalStorage;

namespace Frotcom.Challenge.SendTrackingDataWorker.Classes
{
    public class QueueProcessorFactory : IQueueProcessorFactory
    {
        private readonly IPacketLocalStorage _localStorage;

        public QueueProcessorFactory(IPacketLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public IQueueProcessor Create()
        {
            return new QueueProcessor(_localStorage);
        }
    }
}
