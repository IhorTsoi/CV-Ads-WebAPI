using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services
{
    public class AdvertisementService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IFileStorageService _fileStorageService;

        public AdvertisementService(ApplicationContext dbContext, IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _fileStorageService = fileStorageService;
        }

        public async Task<Advertisement> CreateAdvertisementForCustomer(
            Advertisement advertisement, Stream advertisementPictureStream, Customer customer)
        {
            advertisement.Customer = customer;
            advertisement = await SaveAdvertisementAsync(advertisement);

            await _fileStorageService.SaveFileAsync(advertisement.GetFileName(), advertisementPictureStream);

            return advertisement;
        }

        private async Task<Advertisement> SaveAdvertisementAsync(Advertisement advertisement)
        {
            _dbContext.Advertisements.Add(advertisement);
            await _dbContext.SaveChangesAsync();

            return advertisement;
        }
    }
}
