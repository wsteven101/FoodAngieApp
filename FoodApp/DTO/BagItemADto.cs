using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.DTO
{
    public class BagItemADto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool UpdateData { get; set; }
        public NutritionalContentADto Nutrition { get; set; }
        public IList<FoodItemNodeADto> Foods { get; init; } = new List<FoodItemNodeADto>();
        public IList<BagItemNodeADto> Bags { get; init; } = new List<BagItemNodeADto>();
    }
}
