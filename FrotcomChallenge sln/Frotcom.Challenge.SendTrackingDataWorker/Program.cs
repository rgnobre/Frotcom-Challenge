using System.Threading.Tasks;
using Frotcom.Challenge.Data.Models;
using Frotcom.Challenge.Queue;
using Frotcom.Challenge.SendTrackingDataWorker.Classes;
using Frotcom.Challenge.SendTrackingDataWorker.LocalStorage;
using Frotcom.Challenge.SendTrackingDataWorker.Services;
using Frotcom.Challenge.SendTrackingDataWorker.Services.IServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Frotcom.Challenge.SendTrackingDataWorker
{
    class Program
    {
        /// <summary>
        /// FROTCOM CHALLENGE STARTS HERE
        /// </summary>
        ///
        ///
        public static async Task Main(string[] args)
        {
            //TODO
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ConsoleService>();

                    services.AddSingleton<IConsoleOperationService, ConsoleOperationService>();
                    services.AddSingleton<IQueueProcessorFactory, QueueProcessorFactory>();

                    services.AddSingleton<IPacketLocalStorage, PacketLocalStorage>();
                    services.AddSingleton<IPublishService<Packet>, ConsolePublishService>();
                })
                .RunConsoleAsync();
        }

    }
}
