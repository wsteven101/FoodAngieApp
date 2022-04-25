using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Data.Models;
using FoodDomain.DTO.Repo;
using FoodDomain.Entities;

namespace Food.Data
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Bag, BagRDto>().ReverseMap();
            CreateMap<FoodItem, FoodRDto>().ReverseMap();
            CreateMap<Nutrition, NutritionRDto>().ReverseMap();
            CreateMap<User, UserRDto>().ReverseMap();

            CreateMap<Bag, BagEntity>().ReverseMap();
            CreateMap<FoodItem, FoodEntity>().ReverseMap();
            CreateMap<Nutrition, NutritionalContentEntity>().ReverseMap();
            CreateMap<User, UserEntity>().ReverseMap();

            //CreateMap<BagRDto, Bag>();
            //CreateMap<FoodRDto, FoodItem>();
            //CreateMap<NutritionRDto, Nutrition>();
        }
    }
}
