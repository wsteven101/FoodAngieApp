using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Food.Data.Data;
using FoodDomain.DTO.Repo;
using FoodDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Food.Data.Repos
{
    public class BagRepo : IBagItemRepo
    {
        private FoodAngieContext _foodAngieContext;
        private readonly IMapper _mapper;

        public BagRepo(FoodAngieContext foodAngieContext,
                IMapper mapper)
        {
            _foodAngieContext = foodAngieContext;
            _mapper = mapper;
        }

        public async Task<BagRDto> GetByName(string name)
        {
            var bag = await _foodAngieContext.Bags
                .Include(b => b.Nutrition)
                .Where(b => b.Name == name)
                .FirstOrDefaultAsync();

            var bagDto = _mapper.Map<BagRDto>(bag);
            return bagDto;             
        }

        public async Task<List<BagRDto>> GetBagsByUserId(long userId)
        {
            var userBags = await _foodAngieContext.Bags
                .Include(b => b.Nutrition)
                .Where(b => b.UserId == userId)
                .Join(
                    _foodAngieContext.Users,
                    b => b.UserId, 
                    u => u.Id,
                    (b,u) => b
                )
                .ToListAsync();

            var userBagDtos = _mapper.Map<List<BagRDto>>(userBags);

            return userBagDtos;
        }
    }
}
