using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models.Users.Base;
using System;
using System.Collections.Generic;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class SmartDevice : BaseUser
    {
        private SmartDevice()
        { }

        public SmartDevice(string login, string password)
            : base(login, password, Roles.SMART_DEVICE)
        {
            Mode = SmartDeviceMode.Inactive;
            IsTurnedOn = false;
            IsCaching = false;
            Partner = null;
            AdvertisementViews = new List<AdvertisementView>();
        }

        public SmartDeviceMode Mode { get; set; }
        public bool IsTurnedOn { get; set; }
        public bool IsCaching { get; set; }
        public Guid? PartnerId { get; set; }

        public Partner Partner { get; set; }
        public List<AdvertisementView> AdvertisementViews { get; set; }
    }
}
