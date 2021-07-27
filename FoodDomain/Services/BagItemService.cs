using FoodDomain.Entities;
using FoodDomain.Interfaces;
using FoodDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Services
{
    public class BagItemService : IBagItemService
    {

        private IBagItemRepo _bagRepo;

        public BagItemService(IBagItemRepo bagRepo)
        {
            _bagRepo = bagRepo;
        }


        public async Task<BagItem> GetByName(string name)
        {
            var bagDto = await _bagRepo.GetByName(name);

            return new BagItem(bagDto);
        }

        public async Task<List<BagItem>> GetBagsByUserId(long userId)
        {
            var userBagsDtos = await _bagRepo.GetBagsByUserId(userId);
            var userBagItems = userBagsDtos.Select(b=> new BagItem(b));
            return userBagItems.ToList();
        }

        public void Update(BagItem item)
        {


        }
    }
}

