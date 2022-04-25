using AutoMapper;
using Food.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodApp.DTO;
using FoodDomain.Entities;

namespace FoodApp
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BagEntity, BagItemADto>();
            CreateMap<BagItemNode, BagItemNodeADto>();
            CreateMap<FoodDomain.Entities.FoodEntity, FoodItemADto>();
            CreateMap<FoodItemNode, FoodItemNodeADto>();
            CreateMap<NutritionalContentEntity, NutritionalContentADto>();
            CreateMap<FoodDomain.Entities.UserEntity, UserADto>().ReverseMap();

            CreateMap<BagItemADto, BagEntity>();
            CreateMap<BagItemNodeADto, BagItemNode>();
            CreateMap<FoodItemADto, FoodDomain.Entities.FoodEntity>();
            CreateMap<FoodItemNodeADto, FoodItemNode>();
            CreateMap<NutritionalContentADto, NutritionalContentEntity>();

        }

    }
}
