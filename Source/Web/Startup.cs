using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Infrastructure.Services.Common.Authorization;
using Infrastructure.Services.ContentServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddMemoryCache().AddMvc();

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("LoggedIn", policy => policy.Requirements.Add(new LoggedInRequirement()));
            });

            // Localization
            services.AddLocalization(setup => setup.ResourcesPath = "Resources");
            mvcBuilder
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            Infrastructure.Database.DbConfig.Config.Register(services, Configuration.GetConnectionString("DefaultConnection"));

            //services.AddTransient<IRepository, Repository>();
            //services.AddTransient<IContentServer, ContentServer>();
            //services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            var container = new Container();
            container.Configure(x =>
            {
                x.For<IHttpContextAccessor>().Use<HttpContextAccessor>();
                x.For<IRepository>().Use<Repository>();
                x.For<IContentServer>().Use<ContentServer>();
                x.For<ITempDataProvider>().Use<CookieTempDataProvider>();
                x.Populate(services);
            });
            //services.AddSingleton<StructureMap.IContainer, StructureMap.Container>(x => container);

            Infrastructure.Services.Common.Config.Register(container);
            ModulesCommon.Config.SetupModules(container, mvcBuilder);

            return container.GetInstance<IServiceProvider>();
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

            app.UseStaticFiles();
            app.UseSession();

            app.UseLocalization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
