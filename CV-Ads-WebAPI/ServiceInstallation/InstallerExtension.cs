using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace CV_Ads_WebAPI.ServiceInstallation
{
    public static class InstallerExtension
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly.GetAssembly(typeof(Startup))?
                .GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(IInstaller).IsAssignableFrom(x))
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList()
                .ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
