using System;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class AdvertisementView
    {
        private AdvertisementView()
        { }

        public AdvertisementView(Guid advertisementId, Guid smartDeviceId, string country, string city, int audienceCount)
        {
            Id = Guid.NewGuid();
            AdvertisementId = advertisementId;
            SmartDeviceId = smartDeviceId;
            Country = country;
            City = city;
            AudienceCount = audienceCount;
            DateTime = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        
        public Guid? AdvertisementId { get; set; }
        public Guid? SmartDeviceId { get; set; }

        public DateTime DateTime { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int AudienceCount { get; set; }

        public Advertisement Advertisement { get; set; }
        public SmartDevice SmartDevice { get; set; }
    }
}
