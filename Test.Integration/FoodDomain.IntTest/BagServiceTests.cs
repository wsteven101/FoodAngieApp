using NUnit.Framework;
using Food.Data.Repos;
using AutoMapper;
using Food.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Food.Data;
using FoodDomain.Services;

namespace FoodDomain.IntTest
{
    public class BagServiceTests
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;

        private IMapper _mapper = new MapperConfiguration(cfg =>
        cfg.AddMaps(new[] {
                    typeof(AutoMapperProfiles).Assembly
                })).CreateMapper();

        [SetUp]
        public void Setup()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"; ;
            var builder = new ConfigurationBuilder()
                //SetBasePath("../FoodApp")
                .AddJsonFile("appsettings.JSON", optional: true, reloadOnChange: true)
                //.AddJsonFile($"Config/{envName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();

        }

        [Test]
        public async Task Test_GetByName()
        {
            var sqlConnStr = _configuration.GetConnectionString("FoodAngieConnection");
            sqlConnStr = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=FoodAngie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var contextOptions = new DbContextOptionsBuilder<FoodAngieContext>()
                .UseSqlServer(sqlConnStr)
                .Options;

            using var context = new FoodAngieContext(contextOptions);

            var bagName = (await context.Bags.FirstOrDefaultAsync())?.Name;
            var bagRepo = new BagRepo(context, _mapper);

            var bagService = new BagItemService(bagRepo);
            var bag = await bagService .GetByName(bagName);

            Assert.Greater(bag.Id, 0);
            Assert.AreEqual(bagName, bag.Name);
        }

        [Test]
        public async Task Test_GetBagsByUserId()
        {
            var sqlConnStr = _configuration.GetConnectionString("FoodAngieConnection");
            sqlConnStr = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=FoodAngie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var contextOptions = new DbContextOptionsBuilder<FoodAngieContext>()
                .UseSqlServer(sqlConnStr)
                .Options;

            using var context = new FoodAngieContext(contextOptions);

            var bagUserId = (await context.Bags.FirstOrDefaultAsync())?.UserId;
            var bagRepo = new BagRepo(context, _mapper);

            var bagService = new BagItemService(bagRepo);
            var bags = await bagService.GetBagsByUserId(bagUserId.Value);

            Assert.Greater(bags.Count, 0);
        }
    }
}