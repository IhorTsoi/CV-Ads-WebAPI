using System;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken
{
    public class JWTTokenResponse
    {
        public JWTTokenResponse(string accessToken, DateTime expiresAt)
        {
            AccessToken = accessToken;
            ExpiresAt = expiresAt;
        }

        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
