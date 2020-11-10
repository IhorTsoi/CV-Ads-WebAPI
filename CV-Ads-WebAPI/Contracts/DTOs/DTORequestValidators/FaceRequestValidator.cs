using CV_Ads_WebAPI.Contracts.DTOs.Request;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators
{
    public class FaceRequestValidator : AbstractValidator<FaceRequest>
    {
        public FaceRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.Gender)
                .NotNull()
                .WithMessage(localizer["The gender is required."])
                .IsInEnum()
                .WithMessage(localizer["The gender value provided is not supported."]);

            RuleFor(requset => requset.Age)
                .NotNull()
                .WithMessage(localizer["The age is required."]);
        }
    }
}
