using CarDataRecognizer.ConfigSections;
using Microsoft.Extensions.Options;
using System;

namespace CarDataRecognizer.Utils.Period;

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

        return _config.CleanUnit switch
        {
            "min" or "minute" => TimeSpan.FromMinutes(frequency),
            "hour" => TimeSpan.FromHours(frequency),
            "day" => TimeSpan.FromDays(frequency),
            _ => TimeSpan.FromDays(frequency),
        };
    }

    public DateTime ProvideOldestDatetime()
    {
        int interval = _config.CleanInterval;
        return DateTime.Now.AddDays(-interval);
    }

    public TimeSpan ProvideProcessingPeriod()
    {
        int frequency = _config.DataProcessingFrequency;

        return _config.DataProcessingUnit switch
        {
            "sec" => TimeSpan.FromSeconds(frequency),
            "min" or "minute" => TimeSpan.FromMinutes(frequency),
            "hour" => TimeSpan.FromHours(frequency),
            "day" => TimeSpan.FromDays(frequency),
            _ => TimeSpan.FromDays(frequency),
        };
    }
}

