using AutoMapper;
using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services;
using CV_Ads_WebAPI.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CustomerService _customerService;
        private readonly AdvertisementService _advertisementService;

        public AdvertisementsController(
            IMapper mapper, CustomerService customerService, AdvertisementService advertisementService)
        {
            _mapper = mapper;
            _customerService = customerService;
            _advertisementService = advertisementService;
        }

        [HttpPost(ApiRoutes.Advertisement.CreateAdvertisement)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CUSTOMER)]
        public async Task<IActionResult> CreateAdvertisementAsync([FromForm] CreateAdvertisementRequest createAdvertisementRequest)
        {
            Guid currentUserId = Guid.Parse(User.Identity.Name);
            Customer currentCustomer = await _customerService.GetCustomerByIdAsync(currentUserId);

            Advertisement advertisement = _mapper.Map<Advertisement>(createAdvertisementRequest);

            using (var uploadedImageStream = createAdvertisementRequest.FormFile.OpenReadStream())
            {
                await _advertisementService.CreateAdvertisementForCustomer(
                    advertisement, uploadedImageStream, currentCustomer);
            }

            return Ok();
        }

        [HttpPatch(ApiRoutes.Advertisement.ChangeStatusOfAdvertisement)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CUSTOMER)]
        public async Task<IActionResult> ChangeStatusOfAdvertisement(
            [FromRoute] Guid advertisementId, [FromBody] ChangeStatusOfAdvertisementRequest changeStatusOfAdvertisement)
        {
            Guid currentUserId = Guid.Parse(User.Identity.Name);
            Advertisement advertisement;
            try
            {
                advertisement = await _advertisementService
                    .GetAdvertisementByIdAndCustomerIdAsync(advertisementId, currentUserId);
            }
            catch (Exception e)
            {
                return BadRequest(new BadRequestResponseMessage(e.Message));
            }

            await _advertisementService.UpdateAdvertisementStatus(
                advertisement, (AdvertisementStatus)changeStatusOfAdvertisement.NewAdvertisementStatus);
            return Ok();
        }

        [HttpPost(ApiRoutes.Advertisement.GetAdvertisementByEnvironment)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SMART_DEVICE)]
        public async Task<IActionResult> GetAdvertisementByEnvironmentAsync([FromBody] GetAdvertisementByEnvironmentRequest request)
        {
            int localTimeInMinutes = GetUserLocalTimeInMinutes((int)request.TimeZoneOffset);
            Guid currentUserId = Guid.Parse(User.Identity.Name);

            var response = await _advertisementService.GetAdvertisementByEnvironmentAsync(
                localTimeInMinutes, request, currentUserId);

            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Advertisement.GetAllPersonalAdvertisements)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CUSTOMER)]
        public async Task<IActionResult> GetAllPersonalAdvertisements()
        {
            Guid customerId = Guid.Parse(User.Identity.Name);
            var ads = await _advertisementService.GetAdvertisementsByCustomerIdAsync(customerId);

            var adsDTOs = _mapper.Map<List<AdvertisementResponse>>(ads);
            return Ok(adsDTOs);
        }

        [HttpGet(ApiRoutes.Advertisement.GetViews)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CUSTOMER)]
        public async Task<IActionResult> GetViews([FromRoute] Guid advertisementId)
        {
            Guid customerId = Guid.Parse(User.Identity.Name);
            try
            {
                Advertisement advertisement = await _advertisementService.GetAdvertisementByIdAndCustomerIdAsync(advertisementId, customerId);

                var viewsDTOs = _mapper.Map<List<AdvertisementViewDTO>>(advertisement.AdvertisementViews);
                return Ok(viewsDTOs);
            }
            catch (Exception e)
            {
                return BadRequest(new BadRequestResponseMessage(e.Message));
            }
        }

        private static int GetUserLocalTimeInMinutes(int timeZoneOffset)
        {
            DateTime dateTime = DateTime.UtcNow.AddHours(timeZoneOffset);
            return (dateTime.Hour * 60) + dateTime.Minute;
        }
    }
}
