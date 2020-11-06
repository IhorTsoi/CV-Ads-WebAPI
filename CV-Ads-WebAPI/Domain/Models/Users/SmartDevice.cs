using CV_Ads_WebAPI.Domain.Models.Users.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Domain.Models
{
    public enum SmartDeviceMode
    {
        Inactive,
        Active,
        Blocked
    }

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

        public Partner Partner { get; set; }
        public List<AdvertisementView> AdvertisementViews { get; set; }
    }
}
