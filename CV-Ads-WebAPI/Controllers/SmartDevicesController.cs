using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Reset([FromRoute] Guid smartDeviceId, [FromBody]SmartDeviceReserRequest request)
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
    }
}
