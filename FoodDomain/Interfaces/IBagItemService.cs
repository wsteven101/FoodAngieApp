using FoodDomain.DTO.Repo;
using FoodDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Interfaces
{
    public interface IBagItemService
    {
        public Task<List<BagItem>> GetBagsByUserId(long userId);
        public Task<BagItem> GetByName(string name);
        public void Update(BagItem foodItem);
    }
}
