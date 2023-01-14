using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;
using StoreApp.Services;

namespace StoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<IPhotoService, PhotoService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IRepository<Article>, ArticleRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddDbContextPool<StoreDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("StoreDb"))
            );
            services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreDbContext>();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager
        )
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePages();

            IdentitySeeder.SeedIdentity(roleManager, userManager).GetAwaiter().GetResult();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Shop}/{action=Index}/{id?}"
                );
                endpoints.MapRazorPages();
            });
        }
    }
}
