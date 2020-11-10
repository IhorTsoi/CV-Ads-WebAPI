using System;
using System.Collections.Generic;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class Advertisement
    {
        private Advertisement()
        { }

        public Advertisement
        (
            string name,
            string pictureExtension,
            long viewsLimit,
            string countryScope,
            string cityScope
        )
        {
            Id = Guid.NewGuid();
            Name = name;
            PictureExtension = pictureExtension;
            ViewsCount = 0;
            ViewsLimit = viewsLimit;
            CountryScope = countryScope;
            CityScope = cityScope;
            TimePeriodLimits = new List<TimePeriodLimit>();
            HumanLimits = new List<HumanLimit>();
            AdvertisementViews = new List<AdvertisementView>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PictureExtension { get; set; }
        public long ViewsCount { get; set; }
        public long ViewsLimit { get; set; }
        public string CountryScope { get; set; }
        public string CityScope { get; set; }

        public Customer Customer { get; set; }
        public List<TimePeriodLimit> TimePeriodLimits { get; set; }
        public List<HumanLimit> HumanLimits { get; set; }
        public List<AdvertisementView> AdvertisementViews { get; set; }
    }
}
