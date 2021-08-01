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
        public FoodItem Get(long id);
        public FoodItem GetByName(string name);
        public Task<List<FoodItem>> GetUserFoods(long userId);
        public void Update(FoodItem foodItem);
    }
}
