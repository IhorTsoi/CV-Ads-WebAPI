using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request.DTOsValidators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.Login).NotEmpty()
                .WithMessage(localizer["The login field must not be empty."]);
            RuleFor(request => request.Password).NotEmpty()
                .WithMessage(localizer["The password field must not be empty."]);
        }
    }
}
