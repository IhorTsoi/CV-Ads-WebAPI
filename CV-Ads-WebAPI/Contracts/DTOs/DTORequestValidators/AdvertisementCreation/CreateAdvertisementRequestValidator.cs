using CV_Ads_WebAPI.Contracts.DTOs.Request;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.IO;
using System.Linq;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators.AdvertisementCreation
{
    public class CreateAdvertisementRequestValidator : AbstractValidator<CreateAdvertisementRequest>
    {
        public CreateAdvertisementRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(localizer["The name is required."]);

            RuleFor(request => request.ViewsLimit)
                .NotNull()
                .WithMessage(localizer["The views limit is required."])
                .GreaterThan(0)
                .WithMessage(localizer["The views limit must be greater than 0."]);

            RuleFor(request => request.TimePeriodLimits)
                .NotNull()
                .WithMessage(localizer["The time period limits is required."]);
            RuleForEach(request => request.TimePeriodLimits)
                .SetValidator(new TimePeriodLimitDTOValidator(localizer));

            RuleFor(request => request.HumanLimits)
                .NotNull()
                .WithMessage(localizer["The human limits is required."]);
            RuleForEach(request => request.HumanLimits)
                .SetValidator(new HumanLimitDTOValidator(localizer));

            RuleFor(request => request.FormFile).Must(formFile =>
                formFile != null && !IsEmpty(formFile) && HasPermittedExtension(formFile))
                .WithMessage(localizer["The uploaded file is not valid."]);
        }

        private bool HasPermittedExtension(IFormFile formFile)
        {
            string[] permittedExtensions = { ".jpeg", ".jpg" };
            var extension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(extension) && permittedExtensions.Contains(extension);
        }

        private bool IsEmpty(IFormFile formFile) => formFile.Length == 0;
    }
}
