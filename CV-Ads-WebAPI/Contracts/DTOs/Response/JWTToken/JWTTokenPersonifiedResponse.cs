using CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken.Base;
using System;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken
{
    public class JWTTokenPersonifiedResponse : JWTTokenResponse
    {
        public JWTTokenPersonifiedResponse(string accessToken, DateTime expiresAt, string firstName, string lastName, string role)
            : base(accessToken, expiresAt)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
