using CV_Ads_WebAPI.Contracts.DTOs.Request;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators
{
    public class SmartDeviceResetRequestValidator : AbstractValidator<SmartDeviceResetRequest>
    {
        public SmartDeviceResetRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.NewPassword).NotEmpty()
                .WithMessage(localizer["The new password field must not be empty."]);
        }
    }
}
