using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken;
using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Services.UserServices.Base;
using Microsoft.Extensions.Localization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services.UserServices
{
    public class AdminService : BaseUsersService
    {
        public AdminService
        (
            ApplicationContext dbContext,
            PasswordService passwordService,
            JWTTokenService JWTTokenService,
            IStringLocalizer localizer
        )
            : base(dbContext, JWTTokenService, passwordService, localizer)
        { }

        public async Task<JWTTokenPersonifiedResponse> LoginAdminAsync(LoginRequest loginRequest)
        {
            var identity = await GetUserIdentityAsync(loginRequest);
            var admin = await _dbContext.Admins.FindAsync(identity.Id);
            if (admin == null)
            {
                throw new Exception(_localizer["Login failed. The user is not an admin."]);
            }

            JwtSecurityToken JWTToken = _JWTTokenService.CreateJWTToken(identity);
            string encodedJwt = _JWTTokenService.EncodeJWTToken(JWTToken);

            return new JWTTokenPersonifiedResponse(encodedJwt, JWTToken.ValidTo, admin.FirstName, admin.LastName, identity.Role);
        }
    }
}
