using FoodDomain.DTO.Repo;
using FoodDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Repositories
{
    public interface IBagItemRepo
    {
        public Task<BagEntity> GetByName(string name);
        public Task<List<BagEntity>> GetBagsByUserId(long userId);
        public Task<List<BagEntity>> GetBags(List<long> bagList);
        public Task Update(BagEntity bagDto);
    }
}
