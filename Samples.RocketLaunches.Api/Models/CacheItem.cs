namespace Samples.RocketLaunches.Api.Models
{
    public class CacheItem : CacheItem<object> { }

    public class CacheItem<T>
    {
        public T Item { get; set; }
        public long Expires { get; set; }
        public long Timestamp { get; set; }
    }
}
