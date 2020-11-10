using CV_Ads_WebAPI.Contracts.DTOs.Request;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.IO;
using System.Linq;

namespace CV_Ads_WebAPI.Contracts.DTOs.DTORequestValidators
{
    public class FaceDetectionRequestValidator : AbstractValidator<FaceDetectionRequest>
    {
        public FaceDetectionRequestValidator(IStringLocalizer localizer)
        {
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
