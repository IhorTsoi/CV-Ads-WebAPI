using CV_Ads_WebAPI.Contracts.DTOs.Request;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.IO;
using System.Linq;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators
{
    public class ImportServiceDataRequestValidator : AbstractValidator<ImportServiceDataRequest>
    {
        public ImportServiceDataRequestValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.FormFile).Must(formFile =>
                formFile != null && !IsEmpty(formFile) && HasPermittedExtension(formFile))
                .WithMessage(localizer["The uploaded file is not valid."]);
        }

        private bool HasPermittedExtension(IFormFile formFile)
        {
            string[] permittedExtensions = { ".txt" };
            var extension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(extension) && permittedExtensions.Contains(extension);
        }

        private bool IsEmpty(IFormFile formFile) => formFile.Length == 0;
    }
}
