using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Samples.RocketLaunches.Api.Models;

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

        public static IEnumerable<TOut> Then<TIn, TOut>(this IEnumerable<TIn> source,
                                                        Func<IEnumerable<TIn>, IEnumerable<TOut>> then)
            => then(source);

        public static string ToJson<T>(this T source) => JsonSerializer.Serialize(source);

        public static bool IsExpired(this CacheItem item)
            => item != null && item.Expires > 0 && item.Expires <= DateTime.UtcNow.ToUnixTimestamp();

        public static string ToSha256Base64(this string toHash, Encoding encoding = null)
            => string.IsNullOrEmpty(toHash)
                   ? null
                   : (encoding ?? Encoding.UTF8).GetBytes(toHash).ToSha256Base64();

        public static string ToSha256Base64(this byte[] bytes)
        {
            var hash = bytes.ToSha256();

            if (hash == null || !hash.Any())
            {
                return null;
            }

            return ToBase64(hash);
        }

        public static string ToBase64(this byte[] bytes) => Convert.ToBase64String(bytes);

        public static byte[] ToSha256(this byte[] bytes)
        {
            if (bytes == null || !bytes.Any())
            {
                return null;
            }

            var ha = SHA256.Create();

            if (ha == null)
            {
                return null;
            }

            var hashValue = ha.ComputeHash(bytes);
            ha.Clear();

            return hashValue;
        }

        public static int ToInt(this string source, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(source))
            {
                return defaultValue;
            }

            return int.TryParse(source, out var i)
                       ? i
                       : defaultValue;
        }

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
