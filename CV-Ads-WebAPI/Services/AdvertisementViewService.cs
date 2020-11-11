using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services
{
    public class AdvertisementViewService
    {
        private readonly ApplicationContext _dbContext;

        public AdvertisementViewService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<AdvertisementView>> GetAdvertisementViewsBySmartDevice(SmartDevice smartDevice) =>
            _dbContext.AdvertisementViews.Where(adView => adView.SmartDeviceId == smartDevice.Id)
                .ToListAsync();
    }
}
