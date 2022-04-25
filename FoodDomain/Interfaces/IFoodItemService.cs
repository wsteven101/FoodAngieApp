using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDomain.Entities;

namespace FoodDomain.Interfaces
{
    public interface IFoodItemService
    {
        public FoodEntity Get(long id);
        public FoodEntity GetByName(string name);
        public Task<List<FoodEntity>> GetUserFoods(long userId);
        public void Update(FoodEntity foodItem);
    }
}
