using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class SmartDevicesController : ControllerBase
    {
        private readonly SmartDeviceService _smartDeviceService;

        public SmartDevicesController(SmartDeviceService smartDeviceService)
        {
            _smartDeviceService = smartDeviceService;
        }

        [HttpPost(ApiRoutes.SmartDevice.Login)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var response = await _smartDeviceService.LoginSmartDeviceAsync(loginRequest);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(new BadRequestResponseMessage(exception.Message));
            }
        }

        [HttpPost(ApiRoutes.SmartDevice.Register)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ADMIN)]
        public async Task<IActionResult> Register([FromBody] SmartDeviceRegisterRequest smartDeviceRegisterRequest)
        {
            SmartDevice smartDevice = new SmartDevice
            (
                smartDeviceRegisterRequest.SerialNumber,
                smartDeviceRegisterRequest.Password
            );
            try
            {
                await _smartDeviceService.RegisterUserAsync(smartDevice);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(new BadRequestResponseMessage(exception.Message));
            }
        }

        [HttpPost(ApiRoutes.SmartDevice.Reset)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ADMIN)]
        public async Task<IActionResult> Reset([FromRoute] Guid smartDeviceId, [FromBody] SmartDeviceResetRequest request)
        {
            try
            {
                await _smartDeviceService.ResetSmartDevice(smartDeviceId, request.NewPassword);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(new BadRequestResponseMessage(exception.Message));
            }
        }

        [HttpPost(ApiRoutes.SmartDevice.Activate)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.PARTNER)]
        public async Task<IActionResult> Activate([FromBody] ActivateSmartDeviceRequest activateSmartDeviceRequest)
        {
            Guid partnerId = Guid.Parse(User.Identity.Name);

            try
            {
                await _smartDeviceService.ActivateSmartDeviceAsync(activateSmartDeviceRequest, partnerId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new BadRequestResponseMessage(e.Message));
            }
        }

        [HttpPatch(ApiRoutes.SmartDevice.UpdateConfiguration)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.PARTNER)]
        public async Task<IActionResult> UpdateConfiguration(
            [FromRoute] Guid smartDeviceId, [FromBody] UpdateSmartDeviceConfigurationRequest updateSmartDeviceConfigurationRequest)
        {
            Guid partnerId = Guid.Parse(User.Identity.Name);
            SmartDevice smartDevice = await _smartDeviceService.GetSmartDeviceByIdAndPartnerIdAsync(smartDeviceId, partnerId);
            await _smartDeviceService.UpdateConfigurationAsync(smartDevice, updateSmartDeviceConfigurationRequest);
            return Ok();
        }

        [HttpPatch(ApiRoutes.SmartDevice.Block)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ADMIN)]
        public async Task<IActionResult> Block([FromRoute] Guid smartDeviceId)
        {
            try
            {
                SmartDevice smartDevice = await _smartDeviceService.GetByIdAsync(smartDeviceId);
                await _smartDeviceService.Block(smartDevice);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new BadRequestResponseMessage(e.Message));
            }
        }
    }
}
