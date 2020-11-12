using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CV_Ads_WebAPI.ServiceInstallation
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
