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
            CreateMap<BagItem, BagItemADto>();
            CreateMap<BagItemNode, BagItemNodeADto>();
            CreateMap<FoodDomain.Entities.FoodItem, FoodItemADto>();
            CreateMap<FoodItemNode, FoodItemNodeADto>();
            CreateMap<NutritionalContent, NutritionalContentADto>();
            CreateMap<FoodDomain.Entities.User, UserADto>().ReverseMap();

            CreateMap<BagItemADto, BagItem>();
            CreateMap<BagItemNodeADto, BagItemNode>();
            CreateMap<FoodItemADto, FoodDomain.Entities.FoodItem>();
            CreateMap<FoodItemNodeADto, FoodItemNode>();
            CreateMap<NutritionalContentADto, NutritionalContent>();

        }

    }
}
