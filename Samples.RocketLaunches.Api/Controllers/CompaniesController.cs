using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samples.RocketLaunches.Api.DataAccess;
using Samples.RocketLaunches.Api.Models;
using Samples.RocketLaunches.Api.Services;

namespace Samples.RocketLaunches.Api.Controllers
{
    public class CompaniesController : ApiControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        /// <summary>
        ///     Returns the <see cref="Company" /> with the id specified
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///
        ///         GET /companies/1
        ///
        /// </remarks>
        /// <returns>The <see cref="Company" /> with the id requested</returns>
        /// <response code="200">The <see cref="Company" /> with the id requested</response>
        /// <response code="400">If the request is invalid (will include additional validation error information)</response>
        /// <response code="404">No user record with the id requested</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RlApiResult<Company>> GetCompany(int id)
        {
            var existingUser = _companyRepository.GetById(id);

            return existingUser == null
                       ? NotFound()
                       : existingUser.AsOkRlApiResult();
        }
    }
}
