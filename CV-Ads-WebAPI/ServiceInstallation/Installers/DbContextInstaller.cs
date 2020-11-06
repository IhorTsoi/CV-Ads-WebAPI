using CV_Ads_WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CV_Ads_WebAPI.ServiceInstallation.Installers
{
    public class DbContextInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
#if DEBUG
            string connectionsString = configuration.GetConnectionString("LocalDatabase");
#else
            string connectionsString = configuration.GetConnectionString("RemoteDatabase");
#endif
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionsString));
        }
    }
}
