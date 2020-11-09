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
    public class AdminService : BaseUsersService
    {
        public AdminService
        (
            ApplicationContext dbContext,
            PasswordService passwordService,
            JWTTokenService JWTTokenService
        )
            : base(dbContext, JWTTokenService, passwordService)
        { }

        public async Task<JWTTokenPersonifiedResponse> LoginAdminAsync(LoginRequest loginRequest)
        {
            var identity = await GetUserIdentityAsync(loginRequest);
            var admin = await _dbContext.Admins.FindAsync(identity.Id);
            if (admin == null)
            {
                throw new Exception("Login failed. The user is not an admin.");
            }

            JwtSecurityToken JWTToken = _JWTTokenService.CreateJWTToken(identity);
            string encodedJwt = _JWTTokenService.EncodeJWTToken(JWTToken);

            return new JWTTokenPersonifiedResponse(encodedJwt, JWTToken.ValidTo, admin.FirstName, admin.LastName, identity.Role);
        }
    }
}
