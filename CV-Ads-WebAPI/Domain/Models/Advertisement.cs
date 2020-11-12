using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class Advertisement
    {
        private Advertisement()
        { }

        public Guid Id { get; set; }
        public AdvertisementStatus Status { get; set; }
        public string Name { get; set; }
        public string PictureExtension { get; set; }
        public long ViewsLimit { get; set; }
        public string CountryScope { get; set; }
        public string CityScope { get; set; }
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
        public List<TimePeriodLimit> TimePeriodLimits { get; set; }
        public List<HumanLimit> HumanLimits { get; set; }
        public List<AdvertisementView> AdvertisementViews { get; set; }

        public string GetFileName() => $"{Id}{PictureExtension}";

        public float CalculatePercentageOfViewsLimit()
            => (float)AdvertisementViews.Count / ViewsLimit;

        public int CountTargetAudience(List<FaceRequest> faces) =>
            faces.Count(face => HumanLimits.Any(humanLimit => humanLimit.IsMatch(face)));
    }
}
