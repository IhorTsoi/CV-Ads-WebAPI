using CV_Ads_WebAPI.Contracts.DTOs.RequestResponse;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request
{
    public class CreateAdvertisementRequest
    {
        public string Name { get; set; }

        public long? ViewsLimit { get; set; }
        public string CountryScope { get; set; }
        public string CityScope { get; set; }
        public List<TimePeriodLimitDTO> TimePeriodLimits { get; set; }
        public List<HumanLimitDTO> HumanLimits { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
