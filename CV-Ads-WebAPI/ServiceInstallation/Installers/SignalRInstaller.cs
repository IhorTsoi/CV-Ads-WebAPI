using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CV_Ads_WebAPI.ServiceInstallation.Installers
{
    public class SignalRInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
        }
    }
}
