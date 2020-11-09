using FluentValidation;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request.DTOsValidators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(request => request.Login).NotEmpty().WithMessage("The email field must not be empty.");
            RuleFor(request => request.Password).NotEmpty().WithMessage("The password field must not be empty.");
        }
    }
}
