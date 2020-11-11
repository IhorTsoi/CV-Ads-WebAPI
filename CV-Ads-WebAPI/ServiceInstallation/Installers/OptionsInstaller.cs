using CV_Ads_WebAPI.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CV_Ads_WebAPI.ServiceInstallation.Installers
{
    public class OptionsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTOptions>(configuration.GetSection(JWTOptions.SectionName));
            services.Configure<CultureOptions>(configuration.GetSection(CultureOptions.SectionName));
            services.Configure<FaceDetectionOptions>(configuration.GetSection(FaceDetectionOptions.SectionName));
            services.Configure<AdvertisementEnvironmentDecisionOptions>(
                configuration.GetSection(AdvertisementEnvironmentDecisionOptions.SectionName));
            services.Configure<FinanceOptions>(configuration.GetSection(FinanceOptions.SectionName));
        }
    }
}
