using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators.Registration
{
    public class PartnerRegisterRequestValidator : AbstractValidator<PartnerRegisterRequest>
    {
        public PartnerRegisterRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.Email)
                .NotEmpty()
                .WithMessage(localizer["The email is incorrect."])
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage(localizer["The email is incorrect."]);
            RuleFor(request => request.Password).NotEmpty()
                .WithMessage(localizer["The password field must not be empty."]);
            RuleFor(request => request.FirstName).NotEmpty()
                .WithMessage(localizer["The first name field must not be empty."]);
            RuleFor(request => request.LastName).NotEmpty()
                .WithMessage(localizer["The last name field must not be empty."]);
        }
    }
}
