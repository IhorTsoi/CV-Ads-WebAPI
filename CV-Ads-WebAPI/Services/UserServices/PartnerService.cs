using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken;
using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Services.UserServices.Base;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services.UserServices
{
    public class PartnerService : BaseUsersService
    {
        public PartnerService
        (
            ApplicationContext dbContext,
            PasswordService passwordService,
            JWTTokenService JWTTokenService
        )
            : base(dbContext, JWTTokenService, passwordService)
        { }

        public async Task<JWTTokenPartnerResponse> LoginPartnerAsync(LoginRequest loginRequest)
        {
            var identity = await GetUserIdentityAsync(loginRequest);
            var partner = await _dbContext.Partners.FindAsync(identity.Id);
            if (partner == null)
            {
                throw new Exception("Login failed. The user is not a partner.");
            }

            JwtSecurityToken JWTToken = _JWTTokenService.CreateJWTToken(identity);
            string encodedJwt = _JWTTokenService.EncodeJWTToken(JWTToken);

            return new JWTTokenPartnerResponse(
                encodedJwt, JWTToken.ValidTo, partner.FirstName, partner.LastName, identity.Role, partner.LastWithdrawedDate);
        }
    }
}
