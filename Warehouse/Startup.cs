using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Data.Extension;
using Warehouse.Data.Models;
using Warehouse.Services;
using Warehouse.Services.ApiServices;
using Warehouse.Services.Implementations;


namespace Warehouse
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<WarehouseDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 2;

                    //user settings
                    options.User.RequireUniqueEmail = true;

                    //TODO Stoyan Lupov 23 July, 2019 True
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<WarehouseDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4);

            //Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            //
            //            services.AddTransient<IHtmlSanitizer, HtmlSanitizer>();
            //            services.AddTransient<IHtmlService, HtmlService>();

            //Data services
            //TODO Stoyan Lupov 17 July, 2019 Add all data services to DI Container
            services.AddTransient<IGenericDataService<Company>, GenericDataService<Company>>();
            services.AddTransient<IGenericDataService<BankAccount>, GenericDataService<BankAccount>>();
            services.AddTransient<IGenericDataService<Place>, GenericDataService<Place>>();
            services.AddTransient<IGenericDataService<Payment>, GenericDataService<Payment>>();
            services.AddTransient<IGenericDataService<IncomeExpense>, GenericDataService<IncomeExpense>>();
            services.AddTransient<IGenericDataService<Subscription>, GenericDataService<Subscription>>();
            services.AddTransient<IGenericDataService<ApplicationSettings>, GenericDataService<ApplicationSettings>>();

            //API services
            services.AddTransient<IMerchantRegistryService, BivolMerchantRegistryService>();


            services.AddAutoMapper();
            services.AddRouting(o => o.LowercaseUrls = true);
            services.AddSession();

            services.AddMvc(options =>
            {
                //adds global antiforgery defense for server data alterations from outside
                //TODO Stoyan Lupov 18 July, 2019 Uncomment to enable XSS security -> POST request fail tho
//                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //scope seed db
            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<WarehouseDbContext>();
                context.Database.Migrate();

                CreateRoles(serviceProvider).Wait();
                context.EnsureSeedData();
            }
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "Owner", "Manager" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            //Here you could create a super user who will maintain the web app
            var username = this.Configuration.GetSection("UserSettings")["AdminUsername"];
            var email = this.Configuration.GetSection("UserSettings")["AdminEmail"];

            var superUser = new ApplicationUser
            {
                UserName = username,
                Email = email
            };

            //Ensure you have these values in your appsettings.json or secrets.json file
            var userPwd = this.Configuration.GetSection("UserSettings")["AdminPassword"];
            var user = await userManager.FindByNameAsync(
                this.Configuration.GetSection("UserSettings")["AdminUsername"]);

            var userEmail = this.Configuration.GetSection("UserSettings")["AdminEmail"];

            if (user == null)
            {
                var createSuperUser = await userManager.CreateAsync(superUser, userPwd);
                if (createSuperUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(superUser, "Admin");
                }
            }
        }
    }
}
