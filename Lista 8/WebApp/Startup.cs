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
                // Exercise 1
                endpoints.MapControllerRoute(
                    name: "tool-solve-valid",
                    pattern: "Tool/Solve/{a:double}/{b:double}/{c:double}",
                    defaults: new { controller = "Tool", action = "Solve" }
                );

                // Exercise 2
                endpoints.MapControllerRoute(
                    name: "game-set-valid",
                    pattern: "Set,{n:int:min(1)}",
                    defaults: new { controller = "Game", action = "Set" }
                );

                endpoints.MapControllerRoute(
                    name: "game-draw",
                    pattern: "Draw",
                    defaults: new { controller = "Game", action = "Draw" }
                );

                endpoints.MapControllerRoute(
                    name: "game-guess-valid",
                    pattern: "Guess,{guess:int}",
                    defaults: new { controller = "Game", action = "Guess" }
                );

                // Other
                endpoints.MapControllerRoute(
                    name: "home-index",
                    pattern: "/",
                    defaults: new { controller = "Home", action = "Index" }
                );

                // Errors
                endpoints.MapControllerRoute(
                    name: "tool-solve-invalid",
                    pattern: "Tool/Solve/{**_}",
                    defaults: new { controller = "Error", action = "InvalidSolve" }
                );

                endpoints.MapControllerRoute(
                    name: "game-set-invalid",
                    pattern: "Set,{_}",
                    defaults: new { controller = "Error", action = "InvalidSet" }
                );

                endpoints.MapControllerRoute(
                    name: "game-guess-invalid",
                    pattern: "Guess,{_}",
                    defaults: new { controller = "Error", action = "InvalidGuess" }
                );

                endpoints.MapFallbackToController("PageNotFound", "Error");
            });
        }
    }
}
