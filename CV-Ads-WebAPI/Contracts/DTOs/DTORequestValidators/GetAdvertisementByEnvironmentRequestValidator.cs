using CV_Ads_WebAPI.Contracts.DTOs.Request;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators
{
    public class GetAdvertisementByEnvironmentRequestValidator : AbstractValidator<GetAdvertisementByEnvironmentRequest>
    {
        public GetAdvertisementByEnvironmentRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.Faces)
                .NotEmpty()
                .WithMessage(localizer["Faces is required"]);

            RuleForEach(request => request.Faces)
                .SetValidator(new FaceRequestValidator(localizer));

            RuleFor(request => request.Country)
                .NotEmpty()
                .WithMessage(localizer["Country is required."]);

            RuleFor(request => request.City)
                .NotEmpty()
                .WithMessage(localizer["City is required."]);

            RuleFor(request => request.TimeZoneOffset)
                .NotNull()
                .WithMessage(localizer["Timezone offset is required."]);
        }
    }
}
