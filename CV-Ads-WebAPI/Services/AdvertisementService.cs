using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services
{
    public class AdvertisementService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IFileStorageService _fileStorageService;
        private readonly IStringLocalizer _localizer;

        public AdvertisementService(
            ApplicationContext dbContext, IFileStorageService fileStorageService, IStringLocalizer localizer)
        {
            _dbContext = dbContext;
            _fileStorageService = fileStorageService;
            _localizer = localizer;
        }

        public async Task<Advertisement> CreateAdvertisementForCustomer(
            Advertisement advertisement, Stream advertisementPictureStream, Customer customer)
        {
            advertisement.Customer = customer;
            advertisement = await SaveAdvertisementAsync(advertisement);

            await _fileStorageService.SaveFileAsync(advertisement.GetFileName(), advertisementPictureStream);

            return advertisement;
        }

        public async Task<Advertisement> GetAdvertisementByIdAndCustomerId(Guid advertisementId, Guid customerId)
        {
            Advertisement advertisement = await _dbContext.Advertisements.Include(ad => ad.Customer)
                .FirstOrDefaultAsync(ad => ad.Id == advertisementId);
            if (advertisement == null)
            {
                throw new Exception(_localizer["The advertisement with such id does not exist."]);
            }
            else if (advertisement.Customer.Id != customerId)
            {
                throw new Exception(_localizer["The advertisement with such id does not belong to current customer."]);
            }
            return advertisement;
        }

        public async Task UpdateAdvertisementStatus(Advertisement advertisement, AdvertisementStatus newAdvertisementStatus)
        {
            advertisement.Status = newAdvertisementStatus;

            _dbContext.Advertisements.Update(advertisement);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Advertisement> SaveAdvertisementAsync(Advertisement advertisement)
        {
            _dbContext.Advertisements.Add(advertisement);
            await _dbContext.SaveChangesAsync();

            return advertisement;
        }
    }
}
