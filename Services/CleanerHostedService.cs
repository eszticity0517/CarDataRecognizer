using CarDataRecognizer.Models;
using CarDataRecognizer.Repositories.DataRepository;
using CarDataRecognizer.Utils.Period;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services
{
    /// <summary>
    /// Deletes data from the Data table periodically.
    /// </summary>
    public class CleanerHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;

        private readonly IServiceScopeFactory _scopeFactory;

        private Timer _timer;

        private readonly IPeriodProvider _periodProvider;

        private Task doWorkTask;

        public CleanerHostedService(
            IServiceScopeFactory scopeFactory,
            ILogger<CleanerHostedService> logger,
            IPeriodProvider periodProvider
          )
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _periodProvider = periodProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanerService is starting.");
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, _periodProvider.ProvideCleaningPeriod());

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
            IDataRepository adatRepository = scope.ServiceProvider.GetRequiredService<IDataRepository>();

            // Get entries befor the determined oldest date.
            IQueryable<Data> adatok = adatRepository.GetAllBeforeDateTime(_periodProvider.ProvideOldestDatetime());

            foreach (Data adat in adatok)
            {
                adatRepository.Delete(adat.Id);
            }

            await adatRepository.SaveChangesAsync();

            _logger.LogInformation("Data deletion is finished.");

            _timer.Change(_periodProvider.ProvideCleaningPeriod(), TimeSpan.FromMilliseconds(-1));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanerService is stopping.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
