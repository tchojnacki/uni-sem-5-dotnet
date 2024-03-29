using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreApp.DataContext;
using System.Globalization;

namespace StoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            //services.AddSingleton<IArticleContext, InMemoryCollectionArticleContext>();
            services.AddSingleton<IArticleContext, InMemoryDictionaryArticleContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var culture = CultureInfo.GetCultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Article}/{action=Index}/{id?}"
                );
            });
        }
    }
}
