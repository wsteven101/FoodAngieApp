using FoodDomain.Entities;
using FoodDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Services
{
    public class BagItemService : IBagItemService
    {

        public static Dictionary<string,BagItem> _mockDBList = new Dictionary<string, BagItem>();

        static long _mockNextIdentityNum = 1;

        static BagItemService()
        {
                IFoodItemService s = new FoodItemService();

                var b1 = new BagItem
                {
                    Id = 0,
                    Name = "Favourite",
                    Foods = new List<FoodItemNode>(),
                    Bags = new List<BagItemNode>()
                };
                b1.AddFood( s.Get(1) );
                b1.AddFood( s.Get(2) );

                var b2 = new BagItem
                {
                    Id = 0,
                    Name = "Big Bag",
                    Foods = new List<FoodItemNode>(),
                    Bags = new List<BagItemNode>()
                };
            
                b2.AddFood(s.Get(3));
                b2.AddFood(s.Get(4));
                b2.AddBag(b1);

                _mockDBList[b1.Name] = b1;
                _mockDBList[b2.Name] = b2;

                foreach (var f in _mockDBList)
                {
                    f.Value.Id = GetNextId();
                }
            
        }

        static public long GetNextId()
        {
            return _mockNextIdentityNum++;
        }


        public BagItem GetByName(string name)
        {
            if (_mockDBList.TryGetValue(name, out BagItem item))
            {
                return item;
            }

            return default;
        }

        public void Update(BagItem item)
        {

            if (_mockDBList.TryGetValue(item.Name, out BagItem updateItem))
            {
                _mockDBList.Remove(item.Name);
                item.Id = GetNextId();
            }
            _mockDBList[item.Name] = item;

        }
    }
}

