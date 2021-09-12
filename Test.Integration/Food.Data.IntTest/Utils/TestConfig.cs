using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Data.IntTest.Utils
{
    public class TestConfig
    {
        private IConfiguration _configuration;

        public string GetConfigString(string configName)
        {
            var configValue = GetSecret(configName);
            //var configValue = GetStandardConfig(configName);

            return configValue;
        }

        public string GetConfigConnectionString(string configName)
        {
            configName = "ConnectionStrings--" + configName;
            var configValue = GetSecret(configName);

            return configValue;
        }

        private  string GetSecret(string configName)
        {
            string keyVaultUrl = "https://foodappvault.vault.azure.net";
            string userAssignedClientId = "xxxx";
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });


            var client = new SecretClient(
                vaultUri: new Uri(keyVaultUrl), 
                credential: credential);

            try
            {
                // Retrieve a secret using the secret client.
                var secret = client.GetSecret(configName);

                return secret.Value.Value;
            }
            catch(Exception ex)
            {
                throw;
            }



        }

        private string GetStandardConfig(string configName)
        {
            var builder = new ConfigurationBuilder()
            //SetBasePath("../FoodApp")
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //.AddJsonFile($"Config/{envName}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
            _configuration = builder.Build();

            var configValue = _configuration.GetConnectionString(configName);

            return configValue;
        }
    }
}
