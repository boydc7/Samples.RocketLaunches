using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samples.RocketLaunches.Api.Models;
using Samples.RocketLaunches.Api.Services;

namespace Samples.RocketLaunches.Api.Controllers
{
    public class MetricsController : ApiControllerBase
    {
        private readonly IMetricService _metricService;

        public MetricsController(IMetricService metricService)
        {
            _metricService = metricService;
        }

        /// <summary>
        ///     Returns the <see cref="QueryMetricsResponse" /> that contains a variety of aggregate metrics about the inputs
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///
        ///         GET /metrics?companyId=1&amp;startDate=1950-01-01&amp;endDate=2021-01-01
        ///
        /// </remarks>
        /// <returns>The <see cref="QueryMetricsResponse" /> with the id requested</returns>
        /// <response code="200">The <see cref="Company" /> with the id requested</response>
        /// <response code="400">If the request is invalid (will include additional validation error information)</response>
        /// <response code="404">No user record with the id requested</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RlApiResult<QueryMetricsResponse>> Get([FromQuery] QueryMetricsRequest request)
        {
            var result = _metricService.GetMetrics(request);

            return result.AsOkRlApiResult();
        }
    }
}
