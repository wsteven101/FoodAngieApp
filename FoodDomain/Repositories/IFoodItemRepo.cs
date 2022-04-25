using FoodDomain.DTO.Repo;
using FoodDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Repositories
{
    public interface IFoodItemRepo
    {
        FoodEntity GetByName(string name);
        public Task<List<FoodEntity>> GetFoodsByUserId(long userId);
        public Task<List<FoodEntity>> GetFoods(List<long> foodList);
    }
}
