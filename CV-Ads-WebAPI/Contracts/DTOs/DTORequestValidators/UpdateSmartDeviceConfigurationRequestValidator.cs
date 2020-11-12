using CV_Ads_WebAPI.Contracts.DTOs.Request;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators
{
    public class UpdateSmartDeviceConfigurationRequestValidator : AbstractValidator<UpdateSmartDeviceConfigurationRequest>
    {
        public UpdateSmartDeviceConfigurationRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.IsCaching)
                .NotNull()
                .WithMessage(localizer["Caching mode must be specified."]);

            RuleFor(request => request.IsTurnedOn)
                .NotNull()
                .WithMessage(localizer["The power mode must be specified."]);
        }
    }
}
