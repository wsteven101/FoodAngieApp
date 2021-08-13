﻿using FoodDomain.DTO.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Repositories
{
    public interface IFoodItemRepo
    {
        FoodRDto GetByName(string name);
        public Task<List<FoodRDto>> GetFoodsByUserId(long userId);
        public Task<List<FoodRDto>> GetFoods(List<long> foodList);
    }
}
