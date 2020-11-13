using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.DataAccess
{
    internal class InMemoryStatusRepository : BaseInMemoryModelRepository<Status>, IStatusRepository { }
}
