using System;
using System.Threading.Tasks;
using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Request.Registration;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
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
    }
}
