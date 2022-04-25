using System.Collections.Generic;

namespace FoodDomain.Entities
{

    //public record NutritionalContent(decimal Fat, decimal SaturatedFat, decimal Sugar, decimal Salt);

    public record FoodEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string UserId { get; set; }
        public bool UpdateData { get; set; } = false;
        public NutritionalContentEntity Nutrition { get; set; }

    }

    //public record BagItem(long Id, string Name, List<FoodItem> Foods, List<BagItem> Bags );

    public record FoodItemNode(int Quantity, FoodEntity Food);
    public record BagItemNode(int Quantity, BagEntity Bag);


}