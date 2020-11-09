using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request.DTOsValidators.Registration
{
    public class SmartDeviceRegisterRequestValidator : AbstractValidator<SmartDeviceRegisterRequest>
    {
        public SmartDeviceRegisterRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.SerialNumber).NotEmpty()
                .WithMessage(localizer["The serial number field must not be empty."]);
            RuleFor(request => request.Password).NotEmpty()
                .WithMessage(localizer["The password field must not be empty."]);
        }
    }
}
