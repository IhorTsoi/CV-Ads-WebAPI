using System;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken
{
    public class JWTTokenCustomerResponse : JWTTokenPersonifiedResponse
    {
        public JWTTokenCustomerResponse
        (
            string accessToken,
            DateTime expiresAt,
            string firstName,
            string lastName,
            string role,
            DateTime lastPaidDate
        )
            : base(accessToken, expiresAt, firstName, lastName, role)
        {
            LastPaidDate = lastPaidDate;
        }

        public DateTime LastPaidDate { get; set; }
    }
}
