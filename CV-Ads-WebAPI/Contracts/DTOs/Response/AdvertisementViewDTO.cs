using System;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response
{
    public class AdvertisementViewDTO
    {
        public DateTime DateTime { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int AudienceCount { get; set; }
    }
}
