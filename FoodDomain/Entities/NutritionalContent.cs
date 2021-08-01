using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Entities
{
    public class NutritionalContent
    {
        public long Id { get; set;  }
        public decimal Fat { get; set; } 
        public decimal SaturatedFat { get; set; }
        public decimal Sugar { get; set; }
        decimal Salt { get; set; }
    }

}
