﻿// <auto-generated />
using Food.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Food.Data.Migrations
{
    [DbContext(typeof(FoodAngieContext))]
    partial class FoodAngieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Food.Data.Models.Bag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("ConstituentsJSON")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NutritionId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("NutritionId");

                    b.ToTable("Bag");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Milky Bag",
                            NutritionId = 4L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Mixed Bag",
                            NutritionId = 5L,
                            UserId = 2L
                        });
                });

            modelBuilder.Entity("Food.Data.Models.FoodItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NutritionId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("NutritionId");

                    b.ToTable("Food");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Milky Way",
                            NutritionId = 1L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Malteser Bunny",
                            NutritionId = 2L,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Twisted Creme",
                            NutritionId = 3L,
                            UserId = 2L
                        });
                });

            modelBuilder.Entity("Food.Data.Models.Nutrition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<decimal>("Fat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("FoodId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("SaturatedFat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Sugar")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Nutrition");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Fat = 3m,
                            FoodId = 0L,
                            SaturatedFat = 2m,
                            Sugar = 2m
                        },
                        new
                        {
                            Id = 2L,
                            Fat = 4m,
                            FoodId = 0L,
                            SaturatedFat = 2.5m,
                            Sugar = 2m
                        },
                        new
                        {
                            Id = 3L,
                            Fat = 2m,
                            FoodId = 0L,
                            SaturatedFat = 1m,
                            Sugar = 2.5m
                        },
                        new
                        {
                            Id = 4L,
                            Fat = 9m,
                            FoodId = 0L,
                            SaturatedFat = 4m,
                            Sugar = 3.5m
                        },
                        new
                        {
                            Id = 5L,
                            Fat = 8m,
                            FoodId = 0L,
                            SaturatedFat = 5m,
                            Sugar = 2.7m
                        });
                });

            modelBuilder.Entity("Food.Data.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 2L,
                            FirstName = "John",
                            Surname = "Smith",
                            UserName = "Jonny"
                        },
                        new
                        {
                            Id = 1L,
                            FirstName = "Fred",
                            Surname = "Flintstone",
                            UserName = "Fred"
                        });
                });

            modelBuilder.Entity("Food.Data.Models.Bag", b =>
                {
                    b.HasOne("Food.Data.Models.Nutrition", "Nutrition")
                        .WithMany()
                        .HasForeignKey("NutritionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nutrition");
                });

            modelBuilder.Entity("Food.Data.Models.FoodItem", b =>
                {
                    b.HasOne("Food.Data.Models.Nutrition", "Nutrition")
                        .WithMany()
                        .HasForeignKey("NutritionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nutrition");
                });
#pragma warning restore 612, 618
        }
    }
}
