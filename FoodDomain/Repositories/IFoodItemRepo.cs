using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Repositories
{
    interface IFoodItemRepo
    {
        string Get(string name);
    }
}
