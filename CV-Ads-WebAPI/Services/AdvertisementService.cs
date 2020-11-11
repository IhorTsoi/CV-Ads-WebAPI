using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Domain.Options;
using CV_Ads_WebAPI.Extensions;
using CV_Ads_WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services
{
    public class AdvertisementService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IFileStorageService _fileStorageService;
        private readonly IStringLocalizer _localizer;
        private readonly AdvertisementEnvironmentDecisionOptions _advertisementEnvironmentDecisionOptions;


        public AdvertisementService(
            ApplicationContext dbContext,
            IFileStorageService fileStorageService,
            IStringLocalizer localizer,
            IOptions<AdvertisementEnvironmentDecisionOptions> options
        )
        {
            _dbContext = dbContext;
            _fileStorageService = fileStorageService;
            _localizer = localizer;
            _advertisementEnvironmentDecisionOptions = options.Value;
        }

        public Task<List<Advertisement>> GetAdvertisementsByCustomerIdAsync(Guid customerId) => 
            _dbContext.Advertisements
                .Include(ad => ad.TimePeriodLimits).Include(ad => ad.HumanLimits).Include(ad => ad.AdvertisementViews)
                .Where(ad => ad.CustomerId == customerId).ToListAsync();

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

        public async Task<AdvertisementForSmartDeviceResponse> GetAdvertisementByEnvironmentAsync(
            int localTimeInMinutes, GetAdvertisementByEnvironmentRequest request, Guid smartDeviceId)
        {
            Advertisement[] ads = GetSuitableAdvertisements(localTimeInMinutes, request);
            if (ads.Length == 0)
            {
                return null;
            }

            Advertisement advertisement = GetMostSuitableAdvertisemet(request, ads); ;
            await StoreAdvertisementView(request, smartDeviceId, advertisement);

            var response = new AdvertisementForSmartDeviceResponse(
                advertisement.Name,
                _fileStorageService.GetUrlForFile(advertisement.GetFileName())
            );
            return response;
        }

        private Advertisement[] GetSuitableAdvertisements(int localTimeInMinutes, GetAdvertisementByEnvironmentRequest request)
        {
            return _dbContext.Advertisements.AsQueryable()
                .FilterAdvertisementsByStatus()
                .FilterAdvertisementsByLocation(request)
                .FilterAdvertisementsByTime(localTimeInMinutes)
                .LoadAdvertisementsInMemory()
                .FilterAdvertisementsByHumanLimit(request)
                .FilterAdvertisementsByViewsCountLimit()
                .ToArray();
        }

        private Advertisement GetMostSuitableAdvertisemet(GetAdvertisementByEnvironmentRequest request, Advertisement[] ads)
        {
            float[] humanMatchesNormalizedMarks = GetHumanMatchesCountNormalizedMarks(request, ads);
            float[] remainingViewsNormalizedMarks = GetRemainingViewsNormalizedMarks(ads);

            int recommendedAdIndex = GetMostValuableAdvertisementIndex(humanMatchesNormalizedMarks, remainingViewsNormalizedMarks);
            return ads[recommendedAdIndex];
        }

        private async Task StoreAdvertisementView(
            GetAdvertisementByEnvironmentRequest request, Guid smartDeviceId, Advertisement advertisement)
        {
            _dbContext.AdvertisementViews.Add(new AdvertisementView(
                advertisement.Id,
                smartDeviceId,
                request.Country,
                request.City,
                advertisement.CountTargetAudience(request.Faces)
            ));
            await _dbContext.SaveChangesAsync();
        }

        private float[] GetHumanMatchesCountNormalizedMarks(GetAdvertisementByEnvironmentRequest request, Advertisement[] ads)
        {
            int[] humanMatchesCounts = ads.Select(ad => ad.CountTargetAudience(request.Faces)).ToArray();
            int maxMatchesCount = humanMatchesCounts.Max();

            return humanMatchesCounts.Select(humanMatchesCount => (float)humanMatchesCount / maxMatchesCount).ToArray();
        }

        private float[] GetRemainingViewsNormalizedMarks(Advertisement[] ads)
        {
            var percentagesOfViewsLimit = ads.Select(ad => ad.CalculatePercentageOfViewsLimit());
            return percentagesOfViewsLimit.Select(percentageOfViewsLimit => 1 - percentageOfViewsLimit).ToArray();
        }

        private int GetMostValuableAdvertisementIndex(float[] humanMatchesNormalizedMarks, float[] remainingViewsNormalizedMarks)
        {
            float maximumAdvertisementMark = 0;
            int recommendedAdvertisementIndex = 0;
            for (int i = 0; i < humanMatchesNormalizedMarks.Length; i++)
            {
                float advertisementMark = 0;
                advertisementMark += humanMatchesNormalizedMarks[i] * _advertisementEnvironmentDecisionOptions.AmountOfTargetAudienceWeight;
                advertisementMark += remainingViewsNormalizedMarks[i] * _advertisementEnvironmentDecisionOptions.AmountOfWorkRemainsWeight;

                if (advertisementMark > maximumAdvertisementMark)
                {
                    maximumAdvertisementMark = advertisementMark;
                    recommendedAdvertisementIndex = i;
                }
            }

            return recommendedAdvertisementIndex;
        }
    }
}
