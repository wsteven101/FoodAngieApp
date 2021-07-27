using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Food.Data.Repos;
using AutoMapper;
using Food.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Food.Data.IntTest
{
    class FoodRepoTests
    {
        private IConfiguration _configuration;

        private IMapper _mapper = new MapperConfiguration(cfg =>
        cfg.AddMaps(new[] {
                    typeof(AutoMapperProfiles).Assembly
                })).CreateMapper();

        [SetUp]
        public void Setup()
        {
            // Note the pre-build event copies the appsettings.json file
            // to the test project overwriting any existing one.
            // The appsettings.json file is also marked in the test project as 
            // having Build Action set to "copy always" so that it is then
            // subsequently copied to the ouput directory

            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"; ;
            var builder = new ConfigurationBuilder()
                //SetBasePath("../FoodApp")
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"Config/{envName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();

        }

        [Test]
        public async Task test_food_retrieval()
        {
            var sqlConnStr = _configuration.GetConnectionString("FoodAngieConnection");
            var contextOptions = new DbContextOptionsBuilder<FoodAngieContext>()
                .UseSqlServer(sqlConnStr)
                .Options;

            using var context = new FoodAngieContext(contextOptions);

            var name = (await context.Foods.FirstOrDefaultAsync())?.Name;
            var repo = new FoodRepo(context, _mapper);

            var item = repo.GetByName(name);
            Assert.Greater(item.Id, 0);
            Assert.AreEqual(name, item.Name);
        }

    }
}
