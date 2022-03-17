using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Teamwork_Hackathon.Data;
using Teamwork_Hackathon.Models;
using Teamwork_Hackathon.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.SpaServices.Webpack;

namespace Teamwork_Hackathon
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

            var sqlTeamworkMaster = "server = sunbeam.teamwork.localhost; port = 3306; userid = root; password = dev; database = teamwork_hackathon;";

            services.AddDbContext<ApplicationDbContext>(options =>   options.UseMySql(sqlTeamworkMaster));
            services.AddDbContext<teamwork_hackathonContext>(options => options.UseMySql(sqlTeamworkMaster));
            services.AddIdentity<ApplicationUser, IdentityRole>()
					.AddEntityFrameworkStores<ApplicationDbContext>()
					.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 6;

				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;

				// User settings
				options.User.RequireUniqueEmail = true;
			});

			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.Cookie.Expiration = TimeSpan.FromDays(150);
				options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
				options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
				options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
				options.SlidingExpiration = true;
			});

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();



			// Google Auth
			services.AddAuthentication().AddGoogle(googleOptions =>
			{
                googleOptions.ClientId = "412439077579-6vioj39hpb968ok3232ljt0pacqkc4tf.apps.googleusercontent.com"; //Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = "2uXGTqiq4oDmodhIBDJT93H7"; // Configuration["Authentication:Google:ClientSecret"];
				googleOptions.Scope.Add("profile");

              });

			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            app.UseDatabaseErrorPage();
            app.UseStaticFiles();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                // Webpack initialization with hot-reload.
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                });
            }

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "areas",
                    template: "admin/{controller}/{action}/{id?}",
                    defaults: new { controller = "Config", action = "Index", area = "Admin" }
                );

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });


                routes.MapRoute("404-PageNotFound",
                    "{*url}",
                    new { controller = "StaticContent", action = "PageNotFound" }
                    );
            });
        }
    }
}
