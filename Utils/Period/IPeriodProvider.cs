using System;

namespace CarDataRecognizer.Utils.Period
{
    public interface IPeriodProvider
    {
        /// <summary>
        /// Determines CleanerHostedService period timing.
        /// </summary>
        /// <returns></returns>
        TimeSpan ProvidePeriod();

        /// <summary>
        /// Provides oldest date to maintain data from.
        /// </summary>
        /// <returns></returns>
        DateTime ProvideOldestDatetime();
    }
}
