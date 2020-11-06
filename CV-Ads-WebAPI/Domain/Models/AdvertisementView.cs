using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class AdvertisementView
    {
        private AdvertisementView() 
        { }

        public AdvertisementView
        (
            Advertisement advertisement,
            SmartDevice smartDevice,
            DateTime dateTime,
            string country,
            string city,
            int audienceCount
        )
        {
            Id = Guid.NewGuid();
            Advertisement = advertisement;
            SmartDevice = smartDevice;
            DateTime = dateTime;
            Country = country;
            City = city;
            AudienceCount = audienceCount;
        }

        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int AudienceCount { get; set; }

        public Advertisement Advertisement { get; set; }
        public SmartDevice SmartDevice { get; set; }
    }
}
