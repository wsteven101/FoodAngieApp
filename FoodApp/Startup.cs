using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FoodDomain.Services;

using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using FoodDomain.Interfaces;

namespace FoodApp
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
            services.AddScoped<IFoodItemService, FoodItemService>();
            services.AddScoped<IBagItemService, BagItemService>();
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory

            // add this and package  Microsoft.AspNetCore.SpaServices.Extensions
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // removed redirect for docker so that it doesn't need a https certificate
            // which for a real scenario it should have
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles(); // add for react
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "food-app";
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
