using FluentValidation;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.Validators
{
    public class QueryMetricsRequestValidator : AbstractValidator<QueryMetricsRequest>
    {
        public QueryMetricsRequestValidator()
        {
            RuleFor(e => e.CompanyId)
                .NotNull()
                .Unless(e => e.StartDate.HasValue || e.EndDate.HasValue)
                .WithMessage("At least one of a CompanyId, StartDate, or EndDate must be specified.");

            RuleFor(e => e.EndDate.Value)
                .GreaterThanOrEqualTo(e => e.StartDate.Value)
                .When(e => e.EndDate.HasValue && e.StartDate.HasValue)
                .WithMessage("EndDate, if specified, must be on or after the StartDate");
        }
    }
}
