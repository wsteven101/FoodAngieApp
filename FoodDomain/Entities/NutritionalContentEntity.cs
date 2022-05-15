using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Entities
{
    public class NutritionalContentEntity
    {
        public long Id { get; set;  }
        public decimal Fat { get; set; } 
        public decimal SaturatedFat { get; set; }
        public decimal Sugar { get; set; }
        public decimal Salt { get; set; }

        public void Reset()
        {
            Fat = 0.0m;
            SaturatedFat = 0.0m;
            Sugar = 0.0m;
            Salt = 0.0m;
        }

        public static NutritionalContentEntity operator + (NutritionalContentEntity n1, NutritionalContentEntity n2)
        {
            NutritionalContentEntity n = (NutritionalContentEntity) n1.MemberwiseClone();
            n.Fat += n2.Fat;
            n.SaturatedFat += n2.SaturatedFat;
            n.Sugar += n2.Sugar;
            n.Salt += n2.Salt;
            return n;
        }
    }

}
