using CarDataRecognizer.Models;
using CarDataRecognizer.Repositories.AdatRepository;
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
    /// Meghatározott időközönként törli az önkormányzati adatbázisba már áttöltött adatokat.
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
            _logger.LogInformation("CleanerService indítása.");
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, _periodProvider.ProvidePeriod());

            return Task.CompletedTask;
        }

        private void ExecuteTask(object state)
        {
            // Itt megállítjuk a Timer-t, a DoWork metódus végén pedig újra elindítjuk.
            _timer?.Change(Timeout.Infinite, 0);
            doWorkTask = DoWork();
        }
        private async Task DoWork()
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
            IAdatRepository adatRepository = scope.ServiceProvider.GetRequiredService<IAdatRepository>();

            // Kikérjük az önkormányzati adatbázisba már átmásolt elemeket.
            IQueryable<Adat> adatok = adatRepository.GetAllBeforeDateTime(_periodProvider.ProvideOldestDatetime());

            foreach (Adat adat in adatok)
            {
                adatRepository.Delete(adat.Id);
            }

            await adatRepository.SaveChangesAsync();

            _logger.LogInformation("Az adatok törlése befejeződött.");

            // Befejeződött a feladat, már csak az innen számított következő intervallumban menjen újra a task.
            _timer.Change(_periodProvider.ProvidePeriod(), TimeSpan.FromMilliseconds(-1));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanerService leállítása.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
