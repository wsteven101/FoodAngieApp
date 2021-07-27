using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.DTO.Repo
{
    public class NutritionRDto
    {
        public long Id { get; set; }
        public long FoodId { get; set; }
        public Decimal Fat { get; set; }
        public Decimal SaturatedFat { get; set; }
        public Decimal Sugar { get; set; }
    }
}
