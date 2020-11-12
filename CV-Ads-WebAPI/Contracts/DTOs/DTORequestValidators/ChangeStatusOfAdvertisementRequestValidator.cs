using CV_Ads_WebAPI.Contracts.DTOs.Request;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators
{
    public class ChangeStatusOfAdvertisementRequestValidator : AbstractValidator<ChangeStatusOfAdvertisementRequest>
    {
        public ChangeStatusOfAdvertisementRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.NewAdvertisementStatus)
                .NotNull()
                .WithMessage(localizer["New advertisement status is required."])
                .IsInEnum()
                .WithMessage(localizer["New advertisement status is incorrect."]);
        }
    }
}
