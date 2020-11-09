using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request.DTOsValidators.Registration
{
    public class SmartDeviceRegisterRequestValidator : AbstractValidator<SmartDeviceRegisterRequest>
    {
        public SmartDeviceRegisterRequestValidator()
        {
            RuleFor(request => request.SerialNumber).NotEmpty().WithMessage("The serial number field must not be empty.");
            RuleFor(request => request.Password).NotEmpty().WithMessage("The password field must not be empty.");
        }
    }
}
