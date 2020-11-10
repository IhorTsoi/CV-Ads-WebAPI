using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.IO;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class FaceDetectionController : ControllerBase
    {
        private readonly FaceDetectionService _faceDetectionService;

        public FaceDetectionController(FaceDetectionService faceDetectionService)
        {
            _faceDetectionService = faceDetectionService;
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
