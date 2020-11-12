using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> GetPaymentAmountForCustomerAsync(Customer customer)
        {
            var allAdViews = _dbContext.AdvertisementViews.Where(adView => adView.Advertisement.Customer == customer);
            var notPaidAdViews = allAdViews.Where(adView => adView.DateTime > customer.LastPaidDate);

            int paymentSum = await notPaidAdViews.CountAsync() * _financeOptions.PricePerViewForCustomer;
            return paymentSum;
        }

        public async Task<int> PayAsync(Customer customer)
        {
            int paymentSum = await GetPaymentAmountForCustomerAsync(customer);

            customer.UpdateLastPaidDate();
            await _dbContext.SaveChangesAsync();

            return paymentSum;
        }

        public async Task<int> GetReveneuAmountForPartnerAsync(Partner partner)
        {
            var allAdViews = _dbContext.AdvertisementViews.Where(adView => adView.SmartDevice.Partner == partner);
            var notWithdrawedAdViews = allAdViews.Where(adView => adView.DateTime > partner.LastWithdrawedDate);

            int reveneuSum = await notWithdrawedAdViews.CountAsync() * _financeOptions.PricePerViewForPartner;
            return reveneuSum;
        }

        public async Task<int> WithdrawAsync(Partner partner)
        {
            int reveneuAmount = await GetReveneuAmountForPartnerAsync(partner);

            partner.UpdateLastWithdrawDate();
            await _dbContext.SaveChangesAsync();

            return reveneuAmount;
        }
    }
}
