using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Data.Models
{
    public class FoodItem
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public long NutritionId { get; set; }
        public Nutrition Nutrition { get; set; }

    }
}
