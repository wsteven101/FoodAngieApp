using FoodDomain.DTO.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodDomain.Entities
{
    public class BagItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool UpdateData { get; set; } = false;
        public NutritionalContent Nutrition { get; set; }
        public IList<FoodItemNode> Foods { get; init; } = new List<FoodItemNode>();
        public IList<BagItemNode> Bags { get; init; } = new List<BagItemNode>();

        private string ConstituentsJSON = "";

        public BagItem() 
        {
            RestoreHeiarchy();
        } 

        public BagItem(BagRDto bag)
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

            var bagItem = JsonSerializer.Deserialize<BagItem>(ConstituentsJSON);
            Foods.Clear();
            bagItem.Foods.ToList().ForEach(f => Foods.Add(f));

            Bags.Clear();
            bagItem.Bags.ToList().ForEach(b => Bags.Add(b));
        }


        public void TakeJSONHiearchcySnapshot()
        {
            ConstituentsJSON = JsonSerializer.Serialize<BagItem>(this);
        }

        public void AddFood(FoodItem food, int qty = 1) => Foods.Add(new FoodItemNode(qty, food));
        public void AddBag(BagItem bag, int qty = 1) => Bags.Add(new BagItemNode(qty, bag) );
    }
}
