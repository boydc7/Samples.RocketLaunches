using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samples.RocketLaunches.Api.DataAccess;
using Samples.RocketLaunches.Api.Models;
using Samples.RocketLaunches.Api.Services;

namespace Samples.RocketLaunches.Api.Controllers
{
    public class LocationsController : ApiControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        /// <summary>
        ///     Returns the <see cref="Location" /> with the id specified
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///
        ///         GET /locations/1
        ///
        /// </remarks>
        /// <returns>The <see cref="Location" /> with the id requested</returns>
        /// <response code="200">The <see cref="Location" /> with the id requested</response>
        /// <response code="400">If the request is invalid (will include additional validation error information)</response>
        /// <response code="404">No user record with the id requested</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RlApiResult<Location>> GetLocation(int id)
        {
            var location = _locationRepository.GetById(id);

            return location == null
                       ? NotFound()
                       : location.AsOkRlApiResult();
        }
    }
}
