using AutoMapper;
using FoodDomain.DTO.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDomain.Entities;

namespace FoodDomain.DTO
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<BagItem, BagRDto>();
            //CreateMap<FoodItem, FoodRDto>();
            //CreateMap<NutritionalContent, NutritionRDto>();

            CreateMap<BagRDto, BagItem>();
            CreateMap<FoodRDto,FoodItem>();
            CreateMap<NutritionRDto, NutritionalContent>();
        }
    }
}
