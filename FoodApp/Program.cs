using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //
                // restore key vault once id management on azure
                // is sortrted !!!!!!!! - see section below
                // 
                // Azure Key Vault
                .ConfigureAppConfiguration((context, config) =>
                {
                    var buildConfig = config.Build();
                    config.AddAzureKeyVault(
                        new Uri("https://foodappvault.vault.azure.net"),
                        new DefaultAzureCredential());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
