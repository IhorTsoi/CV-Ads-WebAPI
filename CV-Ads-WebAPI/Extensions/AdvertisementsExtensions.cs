using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CV_Ads_WebAPI.Extensions
{
    public static class AdvertisementsExtensions
    {
        public static IQueryable<Advertisement> FilterAdvertisementsByStatus(this IQueryable<Advertisement> ads) =>
            ads.Where(ad => ad.Status == AdvertisementStatus.Published);

        public static IQueryable<Advertisement> FilterAdvertisementsByLocation(
            this IQueryable<Advertisement> ads, GetAdvertisementByEnvironmentRequest request) =>
            ads.Where(ad => ad.CountryScope == null ||
                (ad.CountryScope == request.Country &&
                (ad.CityScope == null || ad.CityScope == request.City)));

        public static IQueryable<Advertisement> FilterAdvertisementsByTime(
            this IQueryable<Advertisement> ads, int localTimeInMinutes) =>
            ads.Where(ad =>
                ad.TimePeriodLimits.Any(timePeriodLimit =>
                    localTimeInMinutes >= timePeriodLimit.FromInMinutes &&
                    localTimeInMinutes <= timePeriodLimit.ToInMinutes));

        public static IEnumerable<Advertisement> LoadAdvertisementsInMemory(this IQueryable<Advertisement> ads) =>
            ads
                .Include(ad => ad.TimePeriodLimits)
                .Include(ad => ad.HumanLimits)
                .Include(ad => ad.AdvertisementViews)
                .AsEnumerable();

        public static IEnumerable<Advertisement> FilterAdvertisementsByHumanLimit(
            this IEnumerable<Advertisement> ads, GetAdvertisementByEnvironmentRequest request) =>
            ads.Where(ad =>
                ad.HumanLimits.Any(humanLimit =>
                    request.Faces.Any(face => humanLimit.IsMatch(face))));

        public static IEnumerable<Advertisement> FilterAdvertisementsByViewsCountLimit(this IEnumerable<Advertisement> ads) =>
            ads.Where(ad => ad.AdvertisementViews.Count < ad.ViewsLimit);
    }
}
