using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services;
using CV_Ads_WebAPI.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly FinanceService _financeService;

        public CustomersController(CustomerService customerService, FinanceService financeService)
        {
            _customerService = customerService;
            _financeService = financeService;
        }

        [HttpPost(ApiRoutes.Customer.Login)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var response = await _customerService.LoginCustomerAsync(loginRequest);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(new BadRequestResponseMessage(exception.Message));
            }
        }

        [HttpPost(ApiRoutes.Customer.Register)]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterRequest customerRegisterRequest)
        {
            Customer customer = new Customer
            (
                customerRegisterRequest.Email,
                customerRegisterRequest.Password,
                customerRegisterRequest.FirstName,
                customerRegisterRequest.LastName
            );
            try
            {
                await _customerService.RegisterUserAsync(customer);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(new BadRequestResponseMessage(exception.Message));
            }
        }

        [HttpGet(ApiRoutes.Customer.GetPaymentAmount)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.CUSTOMER)]
        public async Task<IActionResult> GetPaymentAmount()
        {
            Guid customerId = Guid.Parse(User.Identity.Name);
            Customer customer = await _customerService.GetCustomerByIdAsync(customerId);

            int paymentAmount = _financeService.GetPaymentAmountForCustomer(customer);
            return Ok(paymentAmount);
        }
    }
}
