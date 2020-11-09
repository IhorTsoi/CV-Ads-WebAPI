using System;

namespace CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken
{
    public class JWTTokenPartnerResponse : JWTTokenPersonifiedResponse
    {
        public JWTTokenPartnerResponse
        (
            string accessToken,
            DateTime expiresAt,
            string firstName,
            string lastName,
            string role,
            DateTime lastWithdrawedDate
        )
            : base(accessToken, expiresAt, firstName, lastName, role)
        {
            LastWithdrawedDate = lastWithdrawedDate;
        }

        public DateTime LastWithdrawedDate { get; set; }
    }
}
