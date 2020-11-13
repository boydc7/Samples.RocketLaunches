using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Samples.RocketLaunches.Api.Services
{
    public static class GenericExtensions
    {
        private static readonly long _maxUnixTimestampInSeconds = int.MaxValue * 3L;

        public static IReadOnlyList<T> AsListReadOnly<T>(this IEnumerable<T> source)
            => source == null
                   ? null
                   : source is IReadOnlyList<T> rol
                       ? rol
                       : AsList(source).AsReadOnly();

        public static List<T> AsList<T>(this IEnumerable<T> source)
            => source == null
                   ? null
                   : source is List<T> sl
                       ? sl
                       : source.ToList();

        public static DateTime? ToDateTime(this string source, DateTime? defaultValue = null)
        {
            if (string.IsNullOrEmpty(source))
            {
                return defaultValue;
            }

            if (long.TryParse(source, out var uts))
            { // Assume this is a unixtimestamp
                return uts.ToDateTime();
            }

            return DateTime.TryParse(source, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dt)
                       ? dt
                       : defaultValue;
        }

        public static DateTime ToDateTime(this long unixTimestamp) => ToDateTime(unixTimestamp, DateTime.UnixEpoch);

        public static DateTime ToDateTime(this long unixTimestamp, DateTime epoch) => epoch.AddSeconds(unixTimestamp > _maxUnixTimestampInSeconds
                                                                                                           ? unixTimestamp / 1000
                                                                                                           : unixTimestamp);

        public static DateTime AsUtc(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime;
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute,
                                dateTime.Second, dateTime.Millisecond, DateTimeKind.Utc);
        }

        public static long ToUnixTimestamp(this DateTime dateTime, DateTime? epoch = null) => (long)dateTime.Subtract(epoch ?? DateTime.UnixEpoch).TotalSeconds;
    }
}
