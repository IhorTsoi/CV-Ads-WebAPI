using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using FluentValidation;
using FluentValidation.Validators;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request.DTOsValidators.Registration
{
    public class AdminRegisterRequestValidator : AbstractValidator<AdminRegisterRequest>
    {
        public AdminRegisterRequestValidator()
        {
            RuleFor(request => request.Email).NotEmpty().EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("The email is incorrect.");
            RuleFor(request => request.Password).NotEmpty().WithMessage("The password field must not be empty.");
            RuleFor(request => request.FirstName).NotEmpty().WithMessage("The first name field must not be empty.");
            RuleFor(request => request.LastName).NotEmpty().WithMessage("The last name field must not be empty.");
        }
    }
}
