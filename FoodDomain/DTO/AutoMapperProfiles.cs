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
            CreateMap<BagEntity, BagRDto>();
            CreateMap<FoodEntity, FoodRDto>();
            CreateMap<NutritionalContentEntity, NutritionRDto>();
            CreateMap<UserEntity, UserRDto>().ReverseMap();

            CreateMap<BagRDto, BagEntity>();
            CreateMap<FoodRDto,FoodEntity>();
            CreateMap<NutritionRDto, NutritionalContentEntity>();
        }
    }
}
