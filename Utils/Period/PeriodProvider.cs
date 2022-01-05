using Kamera.ConfigSections;
using Microsoft.Extensions.Options;
using System;

namespace CarDataRecognizer.Utils.Period
{
    public class PeriodProvider : IPeriodProvider
    {
        private readonly Config _config;

        public PeriodProvider(IOptions<Config> config)
        {
            _config = config.Value;
        }

        public TimeSpan ProvideCleaningPeriod()
        {
            int frequency = _config.CleanFrequency;

            switch (_config.CleanUnit)
            {
                case "min":
                case "minute":
                    return TimeSpan.FromMinutes(frequency);
                case "hour":
                    return TimeSpan.FromHours(frequency);
                case "day":
                    return TimeSpan.FromDays(frequency);
                default:
                    return TimeSpan.FromDays(frequency);
            }
        }

        public DateTime ProvideOldestDatetime()
        {
            int intervallum = _config.CleanInterval;
            return DateTime.Now.AddDays(-intervallum);
        }

        public TimeSpan ProvideProcessingPeriod()
        {
            int frequency = _config.DataProcessingFrequency;

            switch (_config.DataProcessingUnit)
            {
                case "min":
                case "minute":
                    return TimeSpan.FromMinutes(frequency);
                case "hour":
                    return TimeSpan.FromHours(frequency);
                case "day":
                    return TimeSpan.FromDays(frequency);
                default:
                    return TimeSpan.FromDays(frequency);
            }
        }
    }
}
