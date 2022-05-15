﻿using CV_Ads_WebAPI.Services;
using CV_Ads_WebAPI.Services.Interfaces;
using CV_Ads_WebAPI.Services.UserServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace CV_Ads_WebAPI.ServiceInstallation.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AdminService>();
            services.AddTransient<PartnerService>();
            services.AddTransient<CustomerService>();
            services.AddTransient<SmartDeviceService>();
            services.AddTransient<AdvertisementService>();
            services.AddTransient<AdvertisementViewService>();

            services.AddTransient<SmartDeviceHubService>();
            services.AddTransient<IFaceDetectionService, FaceDetectionService>();
            services.AddTransient<FinanceService>();
            services.AddTransient<PasswordService>();
            services.AddTransient<JWTTokenService>();
            services.AddTransient<ServiceDataService>();

            services.AddTransient<ICipherService, CaesarCipherService>();
            services.AddTransient<IStringLocalizer, LocalizationService>();
            services.AddTransient<IFileStorageService, LocalFileStorageService>();
        }
    }
}
