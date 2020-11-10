using CV_Ads_WebAPI.Contracts.DTOs.Request.AdvertisementCreation;
using CV_Ads_WebAPI.Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators.AdvertisementCreation
{
    public class HumanLimitRequestValidator : AbstractValidator<HumanLimitRequest>
    {
        public HumanLimitRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.Gender)
                .NotNull()
                .WithMessage(localizer["The gender is required."])
                .IsInEnum()
                .WithMessage(localizer["The gender value provided is not supported."]);

            RuleFor(request => request.MinAge)
                .NotNull()
                .WithMessage(localizer["The minimum age is required."])
                .GreaterThanOrEqualTo(HumanLimit.MIN_AGE)
                .WithMessage(localizer["The minimum age property cannot be less than 0."]);

            RuleFor(request => request.MaxAge)
                .NotNull()
                .WithMessage(localizer["The maximum age is required."])
                .LessThanOrEqualTo(HumanLimit.MAX_AGE)
                .WithMessage(localizer["The maximum age property cannot be greater than 100."]);
        }
    }
}
