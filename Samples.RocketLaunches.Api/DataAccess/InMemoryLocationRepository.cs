using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.DataAccess
{
    internal class InMemoryLocationRepository : BaseInMemoryModelRepository<Location>, ILocationRepository { }
}
