using CV_Ads_WebAPI.Domain.Constants;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request
{
    public class FaceRequest
    {
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
    }
}
