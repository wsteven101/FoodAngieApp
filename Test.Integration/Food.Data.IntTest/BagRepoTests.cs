using NUnit.Framework;
using Food.Data.Repos;
using AutoMapper;
using Food.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System;
using System.Threading.Tasks;
using Food.Data.IntTest.Utils;

namespace Food.Data.IntTest
{
    public class BagRepoTests
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
        //[TestCase("Milky Way")]
        public async Task test_read_Bag_repo()
        {
            var sqlConnStr = new TestConfig().GetConfigConnectionString("FoodAngieConnection");

            var contextOptions = new DbContextOptionsBuilder<FoodAngieContext>()
                .UseSqlServer(sqlConnStr)
                .Options;

            using var context = new FoodAngieContext(contextOptions);

            var bagName = (await context.Bags.FirstOrDefaultAsync())?.Name;
            var bagRepo = new BagRepo(context, _mapper);

            var bag = await bagRepo .GetByName(bagName);
            Assert.Greater(bag.Id, 0);
            Assert.AreEqual(bagName, bag.Name);
        }

        [Test]
        public async Task test_read_user_bags()
        {
            var sqlConnStr = new TestConfig().GetConfigConnectionString("FoodAngieConnection");
            var contextOptions = new DbContextOptionsBuilder<FoodAngieContext>()
                .UseSqlServer(sqlConnStr)
                .Options;

            using var context = new FoodAngieContext(contextOptions);

            var userId = (await context.Bags.FirstOrDefaultAsync())?.UserId 
                ?? throw new Exception("No bag data read from database, check if table empty");
            var bagRepo = new BagRepo(context, _mapper);
            userId = 2;
            var bagDtos = await bagRepo .GetBagsByUserId(userId);
            Assert.That(bagDtos.Count > 0);
            Assert.Greater(bagDtos[0]?.Id, 0);
            Assert.AreEqual(userId, bagDtos[0]?.UserId);
        }
    }
}