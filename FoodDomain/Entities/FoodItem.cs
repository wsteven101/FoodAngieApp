using System.Collections.Generic;

namespace FoodDomain.Entities
{

    //public record NutritionalContent(decimal Fat, decimal SaturatedFat, decimal Sugar, decimal Salt);

    public record FoodItem
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public bool UpdateData { get; set; } = false;
        public NutritionalContent Nutrition { get; set; }

    }

    //public record BagItem(long Id, string Name, List<FoodItem> Foods, List<BagItem> Bags );

    public record FoodItemNode(int Quantity, FoodItem Food);
    public record BagItemNode(int Quantity, BagItem Bag);


}