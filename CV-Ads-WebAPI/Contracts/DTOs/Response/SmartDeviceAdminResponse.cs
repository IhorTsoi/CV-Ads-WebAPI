using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response
{
    public class SmartDeviceAdminResponse : SmartDevicePartnerResponse
    {
        public string PartnerFirstName { get; set; }
        public string PartnerLastName { get; set; }
        public string PartnerEmail { get; set; }
    }
}
