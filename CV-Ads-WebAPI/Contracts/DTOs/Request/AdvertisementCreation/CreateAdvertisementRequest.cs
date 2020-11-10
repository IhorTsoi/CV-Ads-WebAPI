using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CV_Ads_WebAPI.Contracts.DTOs.Request.AdvertisementCreation
{
    public class CreateAdvertisementRequest
    {
        public string Name { get; set; }

        public long? ViewsLimit { get; set; }
        public string CountryScope { get; set; }
        public string CityScope { get; set; }
        public List<TimePeriodLimitRequest> TimePeriodLimits { get; set; }
        public List<HumanLimitRequest> HumanLimits { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
