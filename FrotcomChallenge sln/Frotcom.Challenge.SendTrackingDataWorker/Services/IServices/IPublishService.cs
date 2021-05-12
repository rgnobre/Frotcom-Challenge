using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frotcom.Challenge.SendTrackingDataWorker.Services.IServices
{
    public interface IPublishService<T>
    {
        Task Post(List<T> obj);
    }
}
