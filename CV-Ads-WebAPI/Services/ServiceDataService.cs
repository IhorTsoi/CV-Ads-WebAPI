using CV_Ads_WebAPI.Contracts.DTOs.RequestResponse;
using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CV_Ads_WebAPI.Services
{
    public class ServiceDataService
    {
        private readonly ApplicationContext _dbContext;
        private readonly ICipherService _cipherService;

        public ServiceDataService(ApplicationContext dbContext, ICipherService cipherService)
        {
            _dbContext = dbContext;
            _cipherService = cipherService;
        }

        public async Task<byte[]> ExportAsync()
        {
            ServiceDataDTO data = await LoadDataToDTOAsync();
            string jsonData = JsonSerializer.Serialize(data);

            string jsonDataEncoded = _cipherService.EncodeString(jsonData);
            return Encoding.UTF8.GetBytes(jsonDataEncoded);
        }

        public async Task ImportAsync(byte[] importFileContent)
        {
            string serviceDataJson = GetOriginalServiceDataJson(importFileContent);
            ServiceDataDTO serviceData = JsonSerializer.Deserialize<ServiceDataDTO>(serviceDataJson);
            await LoadDataToDatabaseAsync(serviceData);
        }

        private async Task<ServiceDataDTO> LoadDataToDTOAsync()
        {
            Guid defaultAdminGuid = new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41");

            return new ServiceDataDTO
            (
                await _dbContext.UserIdentities.AsNoTracking().Where(ui => ui.Id != defaultAdminGuid)
                    .ToListAsync(),
                await _dbContext.Admins.AsNoTracking().Where(ui => ui.Id != defaultAdminGuid)
                    .ToListAsync(),
                await _dbContext.Partners.AsNoTracking().ToListAsync(),
                await _dbContext.Customers.AsNoTracking().ToListAsync(),
                await _dbContext.SmartDevices.AsNoTracking().ToListAsync(),
                await _dbContext.Advertisements.AsNoTracking().ToListAsync(),
                await _dbContext.AdvertisementViews.AsNoTracking().ToListAsync(),
                await _dbContext.HumanLimits.AsNoTracking().ToListAsync(),
                await _dbContext.TimePeriodLimits.AsNoTracking().ToListAsync()
            );
        }
        
        private string GetOriginalServiceDataJson(byte[] importFileContent)
        {
            string serviceDataJsonEncoded = Encoding.UTF8.GetString(importFileContent);
            return _cipherService.DecodeString(serviceDataJsonEncoded);
        }

        private async Task LoadDataToDatabaseAsync(ServiceDataDTO serviceData)
        {
            await _dbContext.UserIdentities.AddRangeAsync(serviceData.UserIdentities);
            await _dbContext.Admins.AddRangeAsync(serviceData.Admins);
            await _dbContext.Customers.AddRangeAsync(serviceData.Customers);
            await _dbContext.Partners.AddRangeAsync(serviceData.Partners);
            await _dbContext.SmartDevices.AddRangeAsync(serviceData.SmartDevices);
            await _dbContext.Advertisements.AddRangeAsync(serviceData.Advertisements);
            await _dbContext.AdvertisementViews.AddRangeAsync(serviceData.AdvertisementViews);
            await _dbContext.TimePeriodLimits.AddRangeAsync(serviceData.TimePeriodLimits);
            await _dbContext.HumanLimits.AddRangeAsync(serviceData.HumanLimits);

            await _dbContext.SaveChangesAsync();
        }

    }
}
