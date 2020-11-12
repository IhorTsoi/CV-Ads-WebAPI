using CV_Ads_WebAPI.Contracts.DTOs.RequestResponse;
using CV_Ads_WebAPI.Domain.Constants;
using System;
using System.Collections.Generic;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response
{
    public class AdvertisementResponse
    {
        public Guid Id { get; set; }
        public AdvertisementStatus Status { get; set; }
        public string Name { get; set; }
        public string PictureLink { get; set; }
        public long ViewsCount { get; set; }
        public long ViewsLimit { get; set; }
        public string CountryScope { get; set; }
        public string CityScope { get; set; }

        public List<TimePeriodLimitDTO> TimePeriodLimits { get; set; }
        public List<HumanLimitDTO> HumanLimits { get; set; }
    }
}
