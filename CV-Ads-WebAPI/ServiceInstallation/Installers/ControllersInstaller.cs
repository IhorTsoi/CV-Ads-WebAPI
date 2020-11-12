using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CV_Ads_WebAPI.ServiceInstallation.Installers
{
    public class ControllersInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((_) => "INVALID_FORMAT"))
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssembly(typeof(Startup).Assembly)); ;
        }
    }
}
