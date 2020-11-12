using CV_Ads_WebAPI.Domain.Constants;
using System;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response
{
    public class SmartDevicePartnerResponse
    {
        public Guid Id { get; set; }
        public SmartDeviceMode Mode { get; set; }
        public bool IsTurnedOn { get; set; }
        public bool IsCaching { get; set; }
        public string SerialNumber { get; set; }
    }
}
