using FoodDomain.DTO.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Entities
{
    public class BagItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public NutritionalContent Nutrition { get; set; }
        public IList<FoodItemNode> Foods { get; init; } = new List<FoodItemNode>();
        public IList<BagItemNode> Bags { get; init; } = new List<BagItemNode>();

        private string ConstituentsJSON = "";

        public BagItem() { } 

        public BagItem(BagRDto bag)
        {
            Id = bag.Id;
            Name = bag.Name;
        }

        public void AddFood(FoodItem food, int qty = 1) => Foods.Add(new FoodItemNode(qty, food));
        public void AddBag(BagItem bag, int qty = 1) => Bags.Add(new BagItemNode(qty, bag) );
    }
}
