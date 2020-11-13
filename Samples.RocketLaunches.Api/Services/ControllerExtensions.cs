using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Services
{
    public static class ControllerExtensions
    {
        public static ActionResult<RlApiResult<T>> AsOkRlApiResult<T>(this T response)
            => new OkObjectResult(AsRlApiResult(response));

        public static ActionResult<RlApiResults<T>> AsOkRlApiResults<T>(this IEnumerable<T> response)
            where T : class
            => new OkObjectResult(AsRlApiResults(response));

        public static RlApiResult<T> AsRlApiResult<T>(this T response)
            => new RlApiResult<T>
               {
                   Result = response
               };

        public static RlApiResults<T> AsRlApiResults<T>(this IEnumerable<T> response)
            where T : class
            => new RlApiResults<T>
               {
                   Results = response.AsListReadOnly()
               };
    }
}
