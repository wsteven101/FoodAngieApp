using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Data.Models;
using FoodDomain.DTO.Repo;

namespace Food.Data
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Bag, BagRDto>();
            CreateMap<FoodItem, FoodRDto>();
            CreateMap<Nutrition, NutritionRDto>();
        }
    }
}
