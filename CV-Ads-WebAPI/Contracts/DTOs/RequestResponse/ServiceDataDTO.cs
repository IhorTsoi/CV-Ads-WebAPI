using CV_Ads_WebAPI.Domain.Models;
using System.Collections.Generic;

namespace CV_Ads_WebAPI.Contracts.DTOs.RequestResponse
{
    public class ServiceDataDTO
    {
        private ServiceDataDTO()
        { }

        public ServiceDataDTO
        (
            List<UserIdentity> userIdentities,
            List<Admin> admins,
            List<Partner> partners,
            List<Customer> customers,
            List<SmartDevice> smartDevices,
            List<Advertisement> advertisements,
            List<AdvertisementView> advertisementViews,
            List<HumanLimit> humanLimits,
            List<TimePeriodLimit> timePeriodLimits
        )
        {
            UserIdentities = userIdentities;
            Admins = admins;
            Partners = partners;
            Customers = customers;
            SmartDevices = smartDevices;
            Advertisements = advertisements;
            AdvertisementViews = advertisementViews;
            HumanLimits = humanLimits;
            TimePeriodLimits = timePeriodLimits;
        }

        public List<UserIdentity> UserIdentities { get; set; }
        public List<Admin> Admins { get; set; }
        public List<Partner> Partners { get; set; }
        public List<Customer> Customers { get; set; }
        public List<SmartDevice> SmartDevices { get; set; }
        public List<Advertisement> Advertisements { get; set; }
        public List<AdvertisementView> AdvertisementViews { get; set; }
        public List<HumanLimit> HumanLimits { get; set; }
        public List<TimePeriodLimit> TimePeriodLimits { get; set; }
    }
}
