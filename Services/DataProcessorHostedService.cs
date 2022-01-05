using CarDataRecognizer.Services.Dir;
using CarDataRecognizer.Utils.Period;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services
{
    public class DataProcessorHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;

        private readonly IServiceScopeFactory _scopeFactory;

        private Timer _timer;

        private readonly IPeriodProvider _periodProvider;

        private Task doWorkTask;

        private IDirectoryService _directoryService;

        public DataProcessorHostedService(
           IServiceScopeFactory scopeFactory,
           ILogger<CleanerHostedService> logger,
           IPeriodProvider periodProvider,
           IDirectoryService directoryService
         )
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _periodProvider = periodProvider;
            _directoryService = directoryService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DataProcessorHostedService is starting.");
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, _periodProvider.ProvideProcessingPeriod());

            return Task.CompletedTask;
        }

        private void ExecuteTask(object state)
        {
            // Stopping timer here, to restart it at the end of DoWork method.
            _timer?.Change(Timeout.Infinite, 0);
            doWorkTask = DoWork();
        }

        private async Task DoWork()
        {
            using IServiceScope scope = _scopeFactory.CreateScope();

            FileInfo[] files = _directoryService.ListFiles();

            foreach (FileInfo file in files)
            { 

            }
 
            _logger.LogInformation("Data processing is finished.");

            _timer.Change(_periodProvider.ProvideProcessingPeriod(), TimeSpan.FromMilliseconds(-1));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DataProcessorHostedService is stopping.");
            return Task.CompletedTask;
        }


        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
