using FoodDomain.DTO.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Repositories
{
    public interface IBagItemRepo
    {
        public Task<BagRDto> GetByName(string name);
        public Task<List<BagRDto>> GetBagsByUserId(long userId);
        public Task<List<BagRDto>> GetBags(List<long> bagList);
        public Task Update(BagRDto bagDto);
    }
}
