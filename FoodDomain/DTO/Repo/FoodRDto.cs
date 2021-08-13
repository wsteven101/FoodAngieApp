using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FoodDomain.DTO.Repo
{
    public class FoodRDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public bool UpdateData { get; set; }
        public long NutritionId { get; set; }
        public NutritionRDto Nutrition { get; set; }
    }
}
