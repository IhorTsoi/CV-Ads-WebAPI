using CV_Ads_WebAPI.Domain.Options;
using CV_Ads_WebAPI.Hubs;
using CV_Ads_WebAPI.ServiceInstallation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Linq;

namespace CV_Ads_WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<CultureOptions> cultureOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            SetCultureConfigurationValues(
                cultureOptions, out string defaultCultureName, out CultureInfo[] supportedCultures);
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCultureName),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SmartDeviceHub>("/smartdevicehub");
            });
        }

        private static void SetCultureConfigurationValues(
            IOptions<CultureOptions> cultureOptions, out string defaultCultureName, out CultureInfo[] supportedCultures)
        {
            CultureOptions cultureOptionsValue = cultureOptions.Value;
            defaultCultureName = cultureOptionsValue.DefaultCulture;
            supportedCultures = cultureOptionsValue.SupportedCultures.Select(culture => new CultureInfo(culture)).ToArray();
        }
    }
}
