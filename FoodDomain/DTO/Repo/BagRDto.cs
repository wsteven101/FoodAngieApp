using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.DTO.Repo
{
    public class BagRDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string ConstituentsJSON { get; set; }
        public long NutritionId { get; set; }

    }
}
