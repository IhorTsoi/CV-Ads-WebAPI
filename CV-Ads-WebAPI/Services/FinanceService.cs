using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Domain.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace CV_Ads_WebAPI.Services
{
    public class FinanceService
    {
        private readonly ApplicationContext _dbContext;
        private readonly FinanceOptions _financeOptions;

        public FinanceService(ApplicationContext dbContext, IOptions<FinanceOptions> options)
        {
            _dbContext = dbContext;
            _financeOptions = options.Value;
        }

        public int GetPaymentAmountForCustomer(Customer customer)
        {
            var allAdViews = _dbContext.AdvertisementViews.Where(adView => adView.Advertisement.Customer == customer);
            var notPaidAdViews = allAdViews.Where(adView => adView.DateTime > customer.LastPaidDate);

            var paymentSum = notPaidAdViews.Count() * _financeOptions.PricePerViewForCustomer;
            return paymentSum;
        }
    }
}
