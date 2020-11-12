using Microsoft.AspNetCore.Http;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request
{
    public class FaceDetectionRequest
    {
        public IFormFile FormFile { get; set; }
    }
}
