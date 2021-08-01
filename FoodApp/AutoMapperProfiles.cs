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
            CreateMap<FoodDomain.Entities.FoodItem, FoodItemADto>();
            CreateMap<NutritionalContent, NutritionalContentADto>();
        }

    }
}
