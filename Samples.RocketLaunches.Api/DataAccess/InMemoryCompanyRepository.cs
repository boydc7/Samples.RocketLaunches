using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.DataAccess
{
    internal class InMemoryCompanyRepository : BaseInMemoryModelRepository<Company>, ICompanyRepository { }
}
