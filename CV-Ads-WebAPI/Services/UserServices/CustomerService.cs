using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Response.JWTToken;
using CV_Ads_WebAPI.Data;
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
    public class CustomerService : BaseUsersService
    {
        public CustomerService
        (
            ApplicationContext dbContext,
            PasswordService passwordService,
            JWTTokenService JWTTokenService,
            IStringLocalizer localizer
        )
            : base(dbContext, JWTTokenService, passwordService, localizer)
        { }

        public async Task<JWTTokenCustomerResponse> LoginCustomerAsync(LoginRequest loginRequest)
        {
            var identity = await GetUserIdentityAsync(loginRequest);
            var customer = await _dbContext.Customers.FindAsync(identity.Id);
            if (customer == null)
            {
                throw new Exception(_localizer["Login failed. The user is not a customer."]);
            }

            JwtSecurityToken JWTToken = _JWTTokenService.CreateJWTToken(identity);
            string encodedJwt = _JWTTokenService.EncodeJWTToken(JWTToken);

            return new JWTTokenCustomerResponse(
                encodedJwt, JWTToken.ValidTo, customer.FirstName, customer.LastName, identity.Role, customer.LastPaidDate);
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            Customer customer = await _dbContext.Customers.Include(customer => customer.Advertisements)
                .FirstOrDefaultAsync(customer => customer.Id == id);
            if (customer == null)
            {
                throw new Exception("The customer could not be found");
            }
            return customer;
        }
    }
}
