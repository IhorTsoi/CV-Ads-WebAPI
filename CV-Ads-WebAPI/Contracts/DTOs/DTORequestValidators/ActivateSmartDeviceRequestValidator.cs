using CV_Ads_WebAPI.Contracts.DTOs.Request;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators
{
    public class ActivateSmartDeviceRequestValidator : AbstractValidator<ActivateSmartDeviceRequest>
    {
        public ActivateSmartDeviceRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.SerialNumber)
                .NotEmpty()
                .WithMessage(localizer["The serial number field must not be empty."]);

            RuleFor(request => request.DefaultPassword)
                .NotEmpty()
                .WithMessage(localizer["The password field must not be empty."]);

            RuleFor(request => request.NewPassword)
                .NotEmpty()
                .WithMessage(localizer["The new password field must not be empty."]);
        }
    }
}
