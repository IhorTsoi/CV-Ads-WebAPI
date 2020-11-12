using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken;
using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.UserServices.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services.UserServices
{
    public class SmartDeviceService : BaseUsersService
    {
        private readonly SmartDeviceHubService _smartDeviceHubService;

        public SmartDeviceService
        (
            ApplicationContext dbContext,
            PasswordService passwordService,
            JWTTokenService JWTTokenService,
            IStringLocalizer localizer,
            SmartDeviceHubService smartDeviceHubService
        )
            : base(dbContext, JWTTokenService, passwordService, localizer)
        {
            _smartDeviceHubService = smartDeviceHubService;
        }

        public async Task<JWTTokenSmartDeviceResponse> LoginSmartDeviceAsync(LoginRequest loginRequest)
        {
            var identity = await GetUserIdentityAsync(loginRequest);
            var smartDevice = await _dbContext.SmartDevices.FindAsync(identity.Id);
            if (smartDevice == null)
            {
                throw new Exception(_localizer["Login failed. The user is not a smart device."]);
            }

            JwtSecurityToken JWTToken = _JWTTokenService.CreateJWTToken(identity);
            string encodedJwt = _JWTTokenService.EncodeJWTToken(JWTToken);

            return new JWTTokenSmartDeviceResponse(
                encodedJwt, JWTToken.ValidTo, smartDevice.Mode, smartDevice.IsTurnedOn, smartDevice.IsCaching);
        }

        public Task<List<SmartDevice>> GetAllAsync() =>
            _dbContext.SmartDevices
                .Include(sd => sd.UserIdentity)
                .Include(sd => sd.Partner).ThenInclude(p => p.UserIdentity)
                .ToListAsync();

        public Task<List<SmartDevice>> GetAllByPartnerIdAsync(Guid partnerId) =>
            _dbContext.SmartDevices
                .Include(sd => sd.UserIdentity)
                .Where(sd => sd.PartnerId == partnerId)
                .ToListAsync();

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

        public async Task ActivateSmartDeviceAsync(ActivateSmartDeviceRequest activateSmartDeviceRequest, Guid partnerId)
        {
            LoginRequest loginRequest = new LoginRequest()
            {
                Login = activateSmartDeviceRequest.SerialNumber,
                Password = activateSmartDeviceRequest.DefaultPassword
            };
            SmartDevice smartDevice = await GetSmartDeviceByLoginPassword(loginRequest);
            if (smartDevice.Mode != SmartDeviceMode.Inactive)
            {
                throw new Exception(_localizer["The smart device cannot be activated."]);
            }

            bool smartDeviceReceivedMessage = await _smartDeviceHubService.TrySendActivateMessageAsync(
                smartDevice.Id, activateSmartDeviceRequest.NewPassword);
            if (!smartDeviceReceivedMessage)
            {
                throw new Exception(_localizer["The smart device is not connected to the service."]);
            }

            smartDevice.Activate(partnerId, _passwordService.GeneratePassword(activateSmartDeviceRequest.NewPassword));
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateConfigurationAsync(
            SmartDevice smartDevice, UpdateSmartDeviceConfigurationRequest updateSmartDeviceConfigurationRequest)
        {
            smartDevice.IsCaching = (bool)updateSmartDeviceConfigurationRequest.IsCaching;
            smartDevice.IsTurnedOn = (bool)updateSmartDeviceConfigurationRequest.IsTurnedOn;
            await _dbContext.SaveChangesAsync();

            await _smartDeviceHubService.SendUpdateMessageAsync(smartDevice.Id);
        }

        public async Task<SmartDevice> GetByIdAsync(Guid smartDeviceId)
        {
            var identity = await _dbContext.UserIdentities.FindAsync(smartDeviceId);
            if (identity == null)
            {
                throw new Exception(_localizer["The user with such id was not found."]);
            }

            var smartDevice = await _dbContext.SmartDevices.FindAsync(identity.Id);
            if (smartDevice == null)
            {
                throw new Exception(_localizer["The user with such id is not a smart device."]);
            }
            return smartDevice;
        }

        public async Task Block(SmartDevice smartDevice)
        {
            smartDevice.Mode = SmartDeviceMode.Blocked;
            await _dbContext.SaveChangesAsync();

            await _smartDeviceHubService.SendUpdateMessageAsync(smartDevice.Id);
        }

        public async Task<SmartDevice> GetSmartDeviceByIdAndPartnerIdAsync(Guid smartDeviceId, Guid partnerId)
        {
            var identity = await _dbContext.UserIdentities.FindAsync(smartDeviceId);
            if (identity == null)
            {
                throw new Exception(_localizer["The user with such id was not found."]);
            }

            var smartDevice = await _dbContext.SmartDevices.FindAsync(identity.Id);
            if (smartDevice == null)
            {
                throw new Exception(_localizer["The user with such id is not a smart device."]);
            }
            else if (smartDevice.PartnerId != partnerId)
            {
                throw new Exception(_localizer["The smart device doesn't belong to user."]);
            }
            return smartDevice;
        }

        private async Task<SmartDevice> GetSmartDeviceByLoginPassword(LoginRequest loginRequest)
        {
            var identity = await GetUserIdentityAsync(loginRequest);

            var smartDevice = await _dbContext.SmartDevices.FindAsync(identity.Id);
            if (smartDevice == null)
            {
                throw new Exception(_localizer["The user with such id is not a smart device."]);
            }
            return smartDevice;
        }
    }
}
