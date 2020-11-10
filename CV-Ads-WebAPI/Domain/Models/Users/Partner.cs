using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models.Users.Base;
using System;
using System.Collections.Generic;

namespace CV_Ads_WebAPI.Domain.Models
{
    public class Partner : BasePersonifiedUser
    {
        private Partner()
        { }

        public Partner(string login, string password, string firstName, string lastName)
            : base(login, password, Roles.PARTNER, firstName, lastName)
        {
            LastWithdrawedDate = DateTime.UtcNow;
            SmartDevices = new List<SmartDevice>();
        }

        public DateTime LastWithdrawedDate { get; set; }

        public List<SmartDevice> SmartDevices { get; set; }
    }
}
