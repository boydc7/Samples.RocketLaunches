using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.DataAccess
{
    internal class InMemoryLaunchRepository : BaseInMemoryModelRepository<Launch>, ILaunchRepository { }
}
