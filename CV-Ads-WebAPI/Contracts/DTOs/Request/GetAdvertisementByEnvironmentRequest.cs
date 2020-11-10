using System.Collections.Generic;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request
{
    public class GetAdvertisementByEnvironmentRequest
    {
        public List<FaceRequest> Faces { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int? TimeZoneOffset { get; set; }
    }
}
