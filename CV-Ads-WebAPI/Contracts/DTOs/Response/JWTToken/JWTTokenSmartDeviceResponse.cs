using CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken.Base;
using CV_Ads_WebAPI.Domain.Constants;
using System;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken
{
    public class JWTTokenSmartDeviceResponse : JWTTokenResponse
    {
        public JWTTokenSmartDeviceResponse(
            string accessToken, DateTime expiresAt, SmartDeviceMode mode, bool isTurnedOn, bool isCaching)
            : base(accessToken, expiresAt)
        {
            Mode = mode;
            IsTurnedOn = isTurnedOn;
            IsCaching = isCaching;
        }

        public SmartDeviceMode Mode { get; set; }
        public bool IsTurnedOn { get; set; }
        public bool IsCaching { get; set; }
    }
}
