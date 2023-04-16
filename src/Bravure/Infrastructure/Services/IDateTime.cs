using System;
using TimeZoneConverter;

namespace Bravure.Infrastructure.Services
{
    public interface IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
        DateTime AustralianEasternStandardDateTime { get; }
        DateTime EndOfTheDayInAustralianEasterStandardDateTime { get; }
    }

    public class DateTimeService : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;

        private static readonly Lazy<TimeZoneInfo> _aestTimeZoneInfo = new Lazy<TimeZoneInfo>(TZConvert.GetTimeZoneInfo("Australia/Sydney"));

        public DateTime AustralianEasternStandardDateTime => TimeZoneInfo.ConvertTimeFromUtc(UtcNow, _aestTimeZoneInfo.Value);

        public DateTime EndOfTheDayInAustralianEasterStandardDateTime => AustralianEasternStandardDateTime.Date.AddDays(1);
    }
}
