using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request.DTOsValidators
{
    public class SmartDeviceResetRequestValidator : AbstractValidator<SmartDeviceReserRequest>
    {
        public SmartDeviceResetRequestValidator()
        {
            RuleFor(request => request.NewPassword).NotEmpty().WithMessage("The new password field must not be empty.");
        }
    }
}
