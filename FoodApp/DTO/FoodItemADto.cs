using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.DTO
{
    public class FoodItemADto
    {
        public long Id { get; set; }

        public string Name { get; init; }

        public NutritionalContentADto Nutrition { get; init; }
    }
}
