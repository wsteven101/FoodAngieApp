using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Data.Models
{
    public class Bag
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public bool UpdateData { get; set; }
        public string ConstituentsJSON { get; set; }
        public long NutritionId { get; set; }
        public Nutrition Nutrition { get; set; }
    }
}
