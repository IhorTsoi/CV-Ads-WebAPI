using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken;
using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.UserServices.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services.UserServices
{
    public class PartnerService : BaseUsersService
    {
        public PartnerService
        (
            ApplicationContext dbContext,
            PasswordService passwordService,
            JWTTokenService JWTTokenService,
            IStringLocalizer localizer
        )
            : base(dbContext, JWTTokenService, passwordService, localizer)
        { }

        public async Task<JWTTokenPartnerResponse> LoginPartnerAsync(LoginRequest loginRequest)
        {
            var identity = await GetUserIdentityAsync(loginRequest);
            var partner = await _dbContext.Partners.FindAsync(identity.Id);
            if (partner == null)
            {
                throw new Exception(_localizer["Login failed. The user is not a partner."]);
            }

            JwtSecurityToken JWTToken = _JWTTokenService.CreateJWTToken(identity);
            string encodedJwt = _JWTTokenService.EncodeJWTToken(JWTToken);

            return new JWTTokenPartnerResponse(
                encodedJwt, JWTToken.ValidTo, partner.FirstName, partner.LastName, identity.Role, partner.LastWithdrawedDate);
        }

        public async Task<Partner> GetPartnerByIdAsync(Guid partnerId)
        {
            Partner partner = await _dbContext.Partners.FirstOrDefaultAsync(p => p.Id == partnerId);
            if (partner == null)
            {
                throw new Exception("The partner could not be found");
            }
            return partner;
        }
    }
}
