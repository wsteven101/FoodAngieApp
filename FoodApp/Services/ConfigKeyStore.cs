using FoodApp.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodApp.Interfaces;

namespace FoodApp.Services
{
    public class ConfigKeyStore: IKeyStore
    {
        private readonly IConfiguration _configuration;

        public ConfigKeyStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string KeyIndex { get => _configuration["Keys:KeyIndex"]; }

        public string GetKey(string name)
        {
            var value = _configuration[name]?.ToString();
            if (value == default)
            {
                 // if not found in key vault or elsewhere (e.g. env vars) search
                 // the appsettings.json config file under the Keys section
                value = _configuration["Keys:"+name].ToString();
            }

            return value;
        }
    }
}
