namespace Samples.RocketLaunches.Api.Models
{
    public interface IHasId<T>
    {
        public T Id { get; set; }
    }

    public interface IHasIntId : IHasId<int> { }
}
