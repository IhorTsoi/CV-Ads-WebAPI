using CV_Ads_WebAPI.Contracts.DTOs.Request.AdvertisementCreation;
using CV_Ads_WebAPI.Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators.AdvertisementCreation
{
    public class TimePeriodLimitRequestValidator : AbstractValidator<TimePeriodLimitRequest>
    {
        public TimePeriodLimitRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.FromInMinutes)
                .NotNull()
                .WithMessage(localizer["The 'from' field of time period is required."])
                .GreaterThanOrEqualTo(TimePeriodLimit.MIN_IN_MINUTES)
                .WithMessage(localizer["The 'from' field of time period cannot be less than 0."]);

            RuleFor(request => request.ToInMinutes)
                .NotNull()
                .WithMessage(localizer["The 'to' field of time period is required."])
                .LessThanOrEqualTo(TimePeriodLimit.MAX_IN_MINUTES)
                .WithMessage(localizer["The 'to' field of time period cannot be greater than 1439."]);
        }
    }
}
