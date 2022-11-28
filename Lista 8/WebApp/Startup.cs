using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using WebApp.Services;

namespace WebApp
{
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IQuadraticSolver, QuadraticSolverAdapter>();
            services.AddSingleton<IRandomQuadraticParams, RandomQuadraticParams>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "tool-solve-valid",
                    pattern: "Tool/Solve/{a:double}/{b:double}/{c:double}",
                    defaults: new { controller = "Tool", action = "Solve" }
                );

                endpoints.MapControllerRoute(
                    name: "tool-solve-invalid",
                    pattern: "Tool/Solve/{**rest}",
                    defaults: new { controller = "Tool", action = "InvalidArguments" }
                );

                endpoints.MapControllerRoute(
                    name: "home-index",
                    pattern: "/",
                    defaults: new { controller = "Home", action = "Index" }
                );

                endpoints.MapFallbackToController("PageNotFound", "Home");
            });
        }
    }
}
