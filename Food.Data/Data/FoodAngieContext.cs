using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Data.Models;


namespace Food.Data.Data
{
    public class FoodAngieContext : DbContext
    {
        public DbSet<FoodItem> Foods { get; set; }
        public DbSet<Bag> Bags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Nutrition> Nutritions { get; set; }

        public FoodAngieContext(DbContextOptions<FoodAngieContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodItem>().ToTable("Food").HasData(
                new FoodItem { Id=1, Name="Milky Way", UserId=1, NutritionId = 1 },
                new FoodItem { Id = 2, Name = "Malteser Bunny", UserId = 1, NutritionId = 2 },
                new FoodItem { Id = 3, Name = "Twisted Creme", UserId = 2, NutritionId = 3 });

            modelBuilder.Entity<Bag>().ToTable("Bag").HasData(
                new Bag { Id=1, Name="Milky Bag", UserId=1, NutritionId=4 },
                new Bag { Id=2, Name="Mixed Bag", UserId=2, NutritionId=5});

            modelBuilder.Entity<User>().ToTable("User").HasData(
                new User { Id=2, FirstName="John", Surname="Smith", UserName="Jonny" },
                new User { Id=1, FirstName="Fred", Surname="Flintstone", UserName="Fred"});

            modelBuilder.Entity<Nutrition>().ToTable("Nutrition").HasData(
                new Nutrition { Id=1, Fat=3, SaturatedFat=2, Sugar=2 },
                new Nutrition { Id = 2, Fat = 4, SaturatedFat = 2.5m, Sugar = 2 },
                new Nutrition { Id = 3, Fat = 2, SaturatedFat = 1, Sugar = 2.5m },
                new Nutrition { Id = 4, Fat = 9, SaturatedFat = 4, Sugar = 3.5m },
                new Nutrition { Id = 5, Fat = 8, SaturatedFat = 5, Sugar = 2.7m });
        }

    }
}
