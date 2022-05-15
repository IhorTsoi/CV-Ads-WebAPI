using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class FaceDetectionController : ControllerBase
    {
        private readonly IFaceDetectionService _faceDetectionService;

        public FaceDetectionController(IFaceDetectionService faceDetectionService)
        {
            _faceDetectionService = faceDetectionService;
        }

        [HttpPost(ApiRoutes.FaceDetection.DetectFacesOnPhoto)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.SMART_DEVICE)]
        public async Task<IActionResult> DetectFacesAsync([FromForm] FaceDetectionRequest faceDetectionRequest)
        {
            await using var imageFileStream = faceDetectionRequest.FormFile.OpenReadStream();
            var faceDetectedResponses = await _faceDetectionService.DetectFaces(imageFileStream);

            return Ok(faceDetectedResponses);
        }
    }
}
