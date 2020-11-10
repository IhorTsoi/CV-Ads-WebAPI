﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain;
using CV_Ads_WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class FaceDetectionController : ControllerBase
    {
        private readonly FaceDetectionService _faceDetectionService;
        private readonly IStringLocalizer _localizer;

        public FaceDetectionController(FaceDetectionService faceDetectionService, IStringLocalizer localizer)
        {
            _faceDetectionService = faceDetectionService;
            _localizer = localizer;
        }

        [HttpPost(ApiRoutes.FaceDetection.DetectFacesOnPhoto)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SMART_DEVICE)]
        public async Task<IActionResult> DetectFacesAsync([FromForm] FaceDetectionRequest faceDetectionRequest)
        {
            using Stream imageFileStream = faceDetectionRequest.FormFile.OpenReadStream();
            return Ok(await _faceDetectionService.DetectFaces(imageFileStream));
        }
    }
}