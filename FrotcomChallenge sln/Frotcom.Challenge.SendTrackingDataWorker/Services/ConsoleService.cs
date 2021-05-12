using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Frotcom.Challenge.SendTrackingDataWorker.Commons;
using Frotcom.Challenge.SendTrackingDataWorker.Enums;
using Frotcom.Challenge.SendTrackingDataWorker.Helpers;
using Frotcom.Challenge.SendTrackingDataWorker.Services.IServices;
using Microsoft.Extensions.Hosting;

namespace Frotcom.Challenge.SendTrackingDataWorker.Services
{
    public class ConsoleService : IHostedService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IConsoleOperationService _consoleOperationService;

        private bool status = true;
        private bool showOptions = true;

        public ConsoleService(IHostApplicationLifetime applicationLifetime, IConsoleOperationService consoleOperationService)
        {
            _applicationLifetime = applicationLifetime;
            _consoleOperationService = consoleOperationService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consoleOperationService.UpdateStatus += cs_UpdateAppStatus;

            _applicationLifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("### Frotcom Challenge ###");

                while (status)
                {
                    if (showOptions)
                        ConsoleWriteOptions();

                    var input = Console.ReadLine();

                    if (InputValidation(input))
                    {
                        switch ((ConsoleOptions)int.Parse(input))
                        {
                            case ConsoleOptions.Run:
                                _consoleOperationService.Run();
                                break;

                            case ConsoleOptions.Stop:
                                _consoleOperationService.Stop();
                                break;

                            case ConsoleOptions.Exit:
                                _consoleOperationService.Exit();
                                break;
                        }
                    }
                }
            });

            return Task.CompletedTask;
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        private void ConsoleWriteOptions()
        {
            foreach (ConsoleOptions option in Enum.GetValues(typeof(ConsoleOptions)))
            {
                Console.WriteLine(EnumHelper.GetDescription(option));
            }
        }

        private bool InputValidation(string input)
        {
            if ((!int.TryParse(input, out int value)) || (!Enum.IsDefined(typeof(ConsoleOptions), value)))
            {
                Console.WriteLine("Invalid option, try again.");
                showOptions = true;
                return false;
            }

            showOptions = false;
            return true;
        }

        private void cs_UpdateAppStatus(object sender, StatusEventArgs e)
        {
            status = e.Status;

            if (!status)
                _applicationLifetime.StopApplication();

        }
    }
}
