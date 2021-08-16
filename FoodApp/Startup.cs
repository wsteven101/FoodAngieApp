using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using FoodDomain.Services;

using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using FoodDomain.Interfaces;
using Food.Data.Data;
using AutoMapper;
using Food.Data.Repos;
using FoodDomain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FoodApp.Interfaces;
using FoodApp.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace FoodApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IKeyStore, ConfigKeyStore>();
            services.AddSingleton<IEncryptionService, AESEncryptionService>();

            services.AddScoped<IFoodItemService, FoodItemService>();
            services.AddScoped<IBagItemService, BagItemService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IBagItemRepo, BagRepo>();
            services.AddScoped<IFoodItemRepo, FoodRepo>();
            services.AddScoped<IUserRepo, UserRepo>();


            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Food App Angie API",
                    Version = "v1",
                    Description = "API for the app FoodAppAngie"
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<FoodAngieContext>(options => options.UseSqlServer(Configuration.GetConnectionString("FoodAngieConnection")));

            var configuration = new MapperConfiguration(cfg =>
                cfg.AddMaps(new[] {
                    "Food.Data"
                })
            );

            var asses = AppDomain.CurrentDomain.GetAssemblies();
            services.AddAutoMapper(asses);

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200",
                                          "http://localhost")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                  });
            });

            /* from code mag
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    
                    options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            */

            //config.JwtToken.Issuer = "https://mysite.com";
            //config.JwtToken.Audience = "https://mysite.com";
            //config.JwtToken.SigningKey = "12345@4321";  //  some long id

            services.AddSession();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                //x.RequireHttpsMetadata = true;
                var jwtConfig = Configuration.GetSection("Jwt");
                var jwtIssuer = jwtConfig["Issuer"].ToString();
                var jwtAudience = jwtConfig["Audience"].ToString();

                 // probably need to move this code into a class
                 // accepting the IKeyStore to retrieve the key
                 // in a common manner
                var jwtKey = Configuration["Keys:JwtKey"].ToString();

                //x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)),
                    ValidAudience = jwtAudience,
                    ValidateAudience = true
                    //ValidateLifetime = true,
                    //ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            // In production, the React files will be served from this directory

            // add this and package  Microsoft.AspNetCore.SpaServices.Extensions
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp";
            //});



        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                 // make sure all end points have an HttpGet/Put etc
                 // as otherwise swagger may not work, check output for exceptions
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

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

            // authorisation
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer" + token);
                }
                await next();
            });

            // removed redirect for docker so that it doesn't need a https certificate
            // which for a real scenario it should have
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSpaStaticFiles(); // add for react
            app.UseRouting();

            // authentication

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.Options.SourcePath = "food-app";
            //        spa.UseReactDevelopmentServer(npmScript: "start");
            //    }
            //});
        }
    }
}
