using Infrastructure.Database.Repository;
using Infrastructure.Services.ContentServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddMvc();

            // Localization
            services.AddLocalization(setup => setup.ResourcesPath = "Resources");
            mvcBuilder
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            Infrastructure.Database.DbConfig.Config.Register(services, Configuration.GetConnectionString("DefaultConnection"));

            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IContentServer, ContentServer>();

            Infrastructure.Services.Common.Config.Register(services);
            ModulesCommon.Config.SetupModules(services, mvcBuilder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo("bg-BG") { NumberFormat = new NumberFormatInfo() { CurrencyDecimalSeparator = ".", NumberDecimalSeparator = "." } },
            };

            var requestCulture = new RequestCulture(culture: "bg-BG", uiCulture: "bg-BG");
            requestCulture.Culture.NumberFormat.CurrencyDecimalSeparator = ".";
            requestCulture.Culture.NumberFormat.NumberDecimalSeparator = ".";

            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = requestCulture,
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
