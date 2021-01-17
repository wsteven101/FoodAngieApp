using FoodDomain.Entities;
using FoodDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Services
{
    public class FoodItemService : IFoodItemService
    {

        static Dictionary<string, FoodItem> _mockDBList = new Dictionary<string, FoodItem> {
            {
                "Cream Egg", new FoodItem() {
                Name = "Cream Egg",
                Nutrition = new NutritionalContent( Fat: 1.5m, SaturatedFat: 0.5m, Salt: 0.2m, Sugar: 5.0m ) } },

            { "Malteser Bunny", new FoodItem() {
                Name = "Malteser Bunny",
                Nutrition = new NutritionalContent( Fat: 2.5m, SaturatedFat: 0.6m, Salt: 0.6m, Sugar: 6.0m ) } },

            { "Flake", new FoodItem() {
                Name = "Flake",
                Nutrition = new NutritionalContent( Fat: 3.5m, SaturatedFat: 0.7m, Salt: 0.7m, Sugar: 8.0m ) } },

            { "Milky Way", new FoodItem() {
                Name = "Milky Way",
                Nutrition = new NutritionalContent( Fat: 4.5m, SaturatedFat: 0.8m, Salt: 0.8m, Sugar: 5.0m ) } },
            };

        static long _mockNextIdentityNum = 1;

        static FoodItemService() 
        {
            foreach (var f in _mockDBList)
            {
                f.Value.Id = GetNextId();
            }
        }

        static public long GetNextId()
        {
            return _mockNextIdentityNum++;
        }

        public FoodItem Get(long id)
        {
            foreach(var f in _mockDBList)
            {
                if (f.Value.Id == id)
                {
                    return f.Value;
                }
            }
            return default;
        }

        public FoodItem GetByName(string name)
        {
            if (_mockDBList.TryGetValue(name, out FoodItem item))
            {
                return item;
            }

            return default;
        }

        public void Update(FoodItem foodItem)
        {

            if ( _mockDBList.TryGetValue(foodItem.Name, out FoodItem updateItem) )
            {
                _mockDBList.Remove(foodItem.Name);
                foodItem.Id = GetNextId();
            }
            _mockDBList[foodItem.Name] = foodItem;

        }
    }
}
