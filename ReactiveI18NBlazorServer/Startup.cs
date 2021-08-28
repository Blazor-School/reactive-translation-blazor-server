using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using ReactiveI18NCookiesBlazorServer.Data;
using System.Collections.Generic;

namespace ReactiveI18NCookiesBlazorServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddScoped(typeof(IStringLocalizer<>), typeof(BlazorSchoolStringLocalizer<>));
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.AddSupportedCultures(new[] { "en", "fr" });
                options.AddSupportedUICultures(new[] { "en", "fr" });
                options.RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    new BlazorSchoolLocalStorageRequestCultureProvider("en")
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRequestLocalization();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
