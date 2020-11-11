using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services;
using CV_Ads_WebAPI.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly PartnerService _partnerService;
        private readonly FinanceService _financeService;

        public PartnersController(PartnerService partnerService, FinanceService financeService)
        {
            _partnerService = partnerService;
            _financeService = financeService;
        }

        [HttpPost(ApiRoutes.Partner.Login)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var response = await _partnerService.LoginPartnerAsync(loginRequest);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(new BadRequestResponseMessage(exception.Message));
            }
        }

        [HttpPost(ApiRoutes.Partner.Register)]
        public async Task<IActionResult> Register([FromBody] PartnerRegisterRequest partnerRegisterRequest)
        {
            Partner partner = new Partner
            (
                partnerRegisterRequest.Email,
                partnerRegisterRequest.Password,
                partnerRegisterRequest.FirstName,
                partnerRegisterRequest.LastName
            );
            try
            {
                await _partnerService.RegisterUserAsync(partner);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(new BadRequestResponseMessage(exception.Message));
            }
        }

        [HttpGet(ApiRoutes.Partner.GetRevenueAmount)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.PARTNER)]
        public async Task<IActionResult> GetReveneuAmount()
        {
            Guid partnerId = Guid.Parse(User.Identity.Name);
            Partner partner = await _partnerService.GetPartnerByIdAsync(partnerId);

            int reveneuAmount = await _financeService.GetReveneuAmountForPartnerAsync(partner);
            return Ok(reveneuAmount);
        }
    }
}
