using CV_Ads_WebAPI.Contracts;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CV_Ads_WebAPI.Controllers
{
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly FinanceOptions _financeOptions;

        public FinanceController(IOptions<FinanceOptions> financeOptions)
        {
            _financeOptions = financeOptions.Value;
        }

        [HttpGet(ApiRoutes.Finance.PricePerViewForCustomer)]
        public IActionResult GetPricePerViewForCustomer() => Ok(_financeOptions.PricePerViewForCustomer);

        [HttpGet(ApiRoutes.Finance.PricePerViewForPartner)]
        public IActionResult PricePerViewForPartner() => Ok(_financeOptions.PricePerViewForPartner);
    }
}
