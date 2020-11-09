using System;
using System.Threading.Tasks;
using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly PartnerService _partnerService;

        public PartnersController(PartnerService partnerService)
        {
            _partnerService = partnerService;
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
    }
}
