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
using System.Linq;
using FoodDomain.Entities;

namespace FoodDomain.IntTest
{
    public class BagServiceTests
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;

        private IMapper _mapper = new MapperConfiguration(cfg =>
        cfg.AddMaps(new[] {
                    typeof(AutoMapperProfiles).Assembly,
                    typeof(FoodDomain.DTO.AutoMapperProfiles).Assembly
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
        public async Task Test_Bag_GetByName()
        {
            var sqlConnStr = _configuration.GetConnectionString("FoodAngieConnection");
            sqlConnStr = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=FoodAngie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var contextOptions = new DbContextOptionsBuilder<FoodAngieContext>()
                .UseSqlServer(sqlConnStr)
                .Options;

            using var context = new FoodAngieContext(contextOptions);

            var bagName = (await context.Bags.FirstOrDefaultAsync())?.Name;
            var bagRepo = new BagRepo(context, _mapper);
            var foodRepo = new FoodRepo(context, _mapper);

            try
            {
                var bagService = new BagItemService(bagRepo,foodRepo,_mapper);
                var bag = await bagService.GetByName(bagName);

                Assert.Greater(bag.Id, 0);
                Assert.AreEqual(bagName, bag.Name);
            }
            catch(Exception ex)
            {
                throw;
            }

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
            var foodRepo = new FoodRepo(context, _mapper);

            try
            {
                var bagService = new BagItemService(bagRepo,foodRepo,_mapper);
                var bags = await bagService.GetBagsByUserId(bagUserId.Value);

                Assert.Greater(bags.Count, 0);
            }
            catch(Exception ex)
            {
                throw;
            }

        }

        [Test]
        public async Task test_fill_out_bag()
        {

            var sqlConnStr = _configuration.GetConnectionString("FoodAngieConnection");
            sqlConnStr = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=FoodAngie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var contextOptions = new DbContextOptionsBuilder<FoodAngieContext>()
                .UseSqlServer(sqlConnStr)
                .Options;

            using var context = new FoodAngieContext(contextOptions);

            var bagUserId = (await context.Bags.FirstOrDefaultAsync())?.UserId;
            var bag = await context.Bags.FirstOrDefaultAsync();
            //var foods = await context.Foods.Take(2).ToListAsync();

            var bagRepo = new BagRepo(context, _mapper);
            var foodRepo = new FoodRepo(context, _mapper);

            try
            {
                var bagService = new BagItemService(bagRepo, foodRepo, _mapper);
                var bags = await bagService.GetBagsByUserId(bagUserId.Value);

                var foodService = new FoodItemService(foodRepo, _mapper);
                var foods = await foodService.GetUserFoods(bagUserId.Value);

                var testBag = bags[0];
                testBag.Foods.Clear();

                foods.ForEach(f => {
                    f.UpdateData = true;
                    f.Name = "";
                    f.Nutrition = null;
                    testBag.Foods.Add(new FoodItemNode(1, f));
                 });

                var childBag = bags[1];
                childBag.UpdateData = true;
                childBag.Name = "";
                childBag.Foods.Clear();

                testBag.Bags.Clear();
                testBag.Bags.Add( new BagItemNode(1,childBag));

                await  bagService.FillBag(testBag);
                Assert.Greater(testBag.Bags[0].Bag.Nutrition.Fat, 0);
                Assert.Greater(testBag.Foods[0].Food.Nutrition.Fat, 0);

                 // json field
                testBag.TakeJSONHiearchcySnapshot();

                // clear
                testBag.Bags.Clear();

                testBag.Foods.Clear();

                testBag.RestoreHeiarchy();

                // re-populate lists from internal json
                await bagService.FillBag(testBag);

                Assert.Greater(testBag.Bags[0].Bag.Nutrition.Fat, 0);
                Assert.Greater(testBag.Foods[0].Food.Nutrition.Fat, 0);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [Test]
        public async Task Test_Bag_Update()
        {
            var sqlConnStr = _configuration.GetConnectionString("FoodAngieConnection");
            sqlConnStr = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=FoodAngie;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var contextOptions = new DbContextOptionsBuilder<FoodAngieContext>()
                .UseSqlServer(sqlConnStr)
                .Options;

            using var context = new FoodAngieContext(contextOptions);

            var bagName = (await context.Bags.
                AsNoTracking()
                .FirstOrDefaultAsync())?.Name;

            var bagRepo = new BagRepo(context, _mapper);
            var foodRepo = new FoodRepo(context, _mapper);

            try
            {
                await context.Database.BeginTransactionAsync();

                var bagService = new BagItemService(bagRepo, foodRepo, _mapper);
                var bag = await bagService.GetByName(bagName);

                var testName = "TestBag";
                var testSugar = 7777.77m;

                var origName = bag.Name;
                var origSugar = bag.Nutrition.Sugar;

                bag.Name = testName;
                bag.Nutrition.Sugar = testSugar;

                await bagService.Update(bag);

                var updatedBag1 = await bagService.GetByName(testName);
                Assert.AreEqual(testSugar, updatedBag1.Nutrition.Sugar);
                Assert.AreEqual(testName, updatedBag1.Name);

                await context.Database.RollbackTransactionAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}