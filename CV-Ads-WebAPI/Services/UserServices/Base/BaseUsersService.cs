using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Domain.Models.Users.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services.UserServices.Base
{
    public abstract class BaseUsersService
    {
        protected readonly ApplicationContext _dbContext;
        protected readonly JWTTokenService _JWTTokenService;
        private readonly PasswordService _passwordService;
        protected IStringLocalizer _localizer;

        protected BaseUsersService(
            ApplicationContext dbContext, JWTTokenService jWTTokenService, PasswordService passwordService, IStringLocalizer localizer)
        {
            _dbContext = dbContext;
            _JWTTokenService = jWTTokenService;
            _passwordService = passwordService;
            _localizer = localizer;
        }

        public async Task RegisterUserAsync<TUser>(TUser user)
            where TUser : BaseUser
        {
            if (await IsUserRegisteredAsync(user))
            {
                throw new Exception(_localizer["The user with such login is already registered."]);
            }

            SecurePassword(user);

            await _dbContext.Set<TUser>().AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        protected async Task<UserIdentity> GetUserIdentityAsync(LoginRequest loginRequest)
        {
            UserIdentity userIdentity = await GetUserIdentityByLoginAsync(loginRequest.Login);
            if (userIdentity == null)
            {
                throw new Exception(_localizer["The user with such login doesn't exist"]);
            }

            bool isPasswordCorrect = _passwordService.VerifyHashedPassword(
                userIdentity.Password, loginRequest.Password);
            if (!isPasswordCorrect)
            {
                throw new Exception(_localizer["The password is not correct"]);
            }

            return userIdentity;
        }

        private async Task<bool> IsUserRegisteredAsync<TUser>(TUser user)
            where TUser : BaseUser
        {
            UserIdentity registeredIdentity = await GetUserIdentityByLoginAsync(user.UserIdentity.Login);
            bool isAlreadyRegistered = registeredIdentity != null;
            return isAlreadyRegistered;
        }

        private async Task<UserIdentity> GetUserIdentityByLoginAsync(string login) =>
            await _dbContext.UserIdentities.FirstOrDefaultAsync(identity => identity.Login == login);

        private void SecurePassword<TUser>(TUser user) where TUser : BaseUser =>
            user.UserIdentity.Password = _passwordService.GeneratePassword(user.UserIdentity.Password);
    }
}
