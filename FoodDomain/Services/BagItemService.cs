using AutoMapper;
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
        private readonly IMapper _mapper;
        private IBagItemRepo _bagRepo;

        public BagItemService(IBagItemRepo bagRepo, IMapper mapper)
        {
            _bagRepo = bagRepo;
            _mapper = mapper;
        }


        public async Task<BagItem> GetByName(string name)
        {
            var bagDto = await _bagRepo.GetByName(name);
            var bagItem = _mapper.Map<BagItem>(bagDto);

            return bagItem;
        }

        public async Task<List<BagItem>> GetBagsByUserId(long userId)
        {
            var userBagsDtos = (await _bagRepo.GetBagsByUserId(userId)).ToList();
            var userBagItems = _mapper.Map<List<BagItem>>(userBagsDtos);
            return userBagItems.ToList();
        }

        public void Update(BagItem item)
        {


        }
    }
}

