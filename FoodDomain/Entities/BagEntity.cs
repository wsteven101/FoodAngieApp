using FoodDomain.DTO.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodDomain.Entities
{
    public class BagEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public bool UpdateData { get; set; } = false;
        public NutritionalContentEntity Nutrition { get; set; }
        public IList<FoodItemNode> Foods { get; init; } = new List<FoodItemNode>();
        public IList<BagItemNode> Bags { get; init; } = new List<BagItemNode>();

        private string ConstituentsJSON = "";

        public BagEntity() 
        {
            RestoreHeiarchy();
        } 

        public BagEntity(BagRDto bag)
        {
            Id = bag.Id;
            Name = bag.Name;
            ConstituentsJSON = bag.ConstituentsJSON;

            RestoreHeiarchy();
        }

        public void RestoreHeiarchy()
        {
            if (ConstituentsJSON.Trim() == "")
            {
                return;
            }

            var bagItem = JsonSerializer.Deserialize<BagEntity>(ConstituentsJSON);
            Foods.Clear();
            bagItem.Foods.ToList().ForEach(f => Foods.Add(f));

            Bags.Clear();
            bagItem.Bags.ToList().ForEach(b => Bags.Add(b));
        }


        public void TakeJSONHiearchcySnapshot()
        {
            ConstituentsJSON = JsonSerializer.Serialize<BagEntity>(this);
        }

        public void AddFood(FoodEntity food, int qty = 1) => Foods.Add(new FoodItemNode(qty, food));
        public void AddBag(BagEntity bag, int qty = 1) => Bags.Add(new BagItemNode(qty, bag) );
    }
}
