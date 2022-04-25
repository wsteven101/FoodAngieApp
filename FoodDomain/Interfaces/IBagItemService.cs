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
        public Task<List<BagEntity>> GetBagsByUserId(long userId);
        public Task<BagEntity> GetByName(string name);
        public Task FillBag(BagEntity bag);
        public Task Update(BagEntity foodItem);
    }
}
