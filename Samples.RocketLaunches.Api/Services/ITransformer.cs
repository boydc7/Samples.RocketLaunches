namespace Samples.RocketLaunches.Api.Services
{
    public interface ITransformer<in TIn, out TOut>
        where TIn : class
        where TOut : class
    {
        TOut To(TIn source);
    }
}
