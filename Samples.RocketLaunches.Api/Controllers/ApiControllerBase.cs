using Microsoft.AspNetCore.Mvc;

namespace Samples.RocketLaunches.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ConflictObjectResult Conflict(string modelType, string modelField)
            => new ConflictObjectResult(new
                                        {
                                            Message = $"A {modelType} record with the {modelField} specified already exists."
                                        });
    }
}
