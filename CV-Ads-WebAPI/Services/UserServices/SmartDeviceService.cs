using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken;
using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.UserServices.Base;
using Microsoft.Extensions.Localization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services.UserServices
{
    public class SmartDeviceService : BaseUsersService
    {
        public SmartDeviceService
        (
            ApplicationContext dbContext,
            PasswordService passwordService,
            JWTTokenService JWTTokenService,
            IStringLocalizer localizer
        )
            : base(dbContext, JWTTokenService, passwordService, localizer)
        { }

        public async Task<JWTTokenResponse> LoginSmartDeviceAsync(LoginRequest loginRequest)
        {
            var identity = await GetUserIdentityAsync(loginRequest);
            var smartDevice = await _dbContext.SmartDevices.FindAsync(identity.Id);
            if (smartDevice == null)
            {
                throw new Exception(_localizer["Login failed. The user is not a smart device."]);
            }

            JwtSecurityToken JWTToken = _JWTTokenService.CreateJWTToken(identity);
            string encodedJwt = _JWTTokenService.EncodeJWTToken(JWTToken);

            return new JWTTokenResponse(encodedJwt, JWTToken.ValidTo);
        }

        public async Task ResetSmartDevice(Guid id, string newPassword)
        {
            var identity = await _dbContext.UserIdentities.FindAsync(id);
            if (identity == null)
            {
                throw new Exception(_localizer["The user with such id was not found."]);
            }

            var smartDevice = await _dbContext.SmartDevices.FindAsync(identity.Id);
            if (smartDevice == null)
            {
                throw new Exception(_localizer["The user with such id is not a smart device."]);
            }

            string login = identity.Login;
            await DeleteSmartDevice(identity, smartDevice);
            await RegisterUserAsync(new SmartDevice(login, newPassword));
        }

        private async Task DeleteSmartDevice(UserIdentity identity, SmartDevice smartDevice)
        {
            _dbContext.Remove(smartDevice);
            await _dbContext.SaveChangesAsync();
            
            _dbContext.Remove(identity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
