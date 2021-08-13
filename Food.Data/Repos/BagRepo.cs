using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Food.Data.Data;
using Food.Data.Models;
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
                .AsNoTracking()
                .Include(b => b.Nutrition)
                .Where(b => b.Name == name)
                .FirstOrDefaultAsync();

            var bagDto = _mapper.Map<BagRDto>(bag);
            return bagDto;             
        }

        public async Task<List<BagRDto>> GetBagsByUserId(long userId)
        {
            var userBags = await _foodAngieContext.Bags
                .AsNoTracking()
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

        public async Task<List<BagRDto>> GetBags(List<long> bagList)
        {
            var bags = await _foodAngieContext.Bags
                .AsNoTracking()
                .Include(b => b.Nutrition)
                .Where(b => bagList.Contains(b.Id))
                .ToListAsync();

            var bagDtos = _mapper.Map<List<BagRDto>>(bags);
            return bagDtos;
        }

        public async Task Update(BagRDto bagDto)
        {
             // disconnected save
             try
            {
                var bag = _mapper.Map<Bag>(bagDto);
                var origBag = _foodAngieContext.Update(bag);
                await _foodAngieContext.SaveChangesAsync();

                 // detach entities so that update can be applied more than once by integration tests
                _foodAngieContext.Entry<Bag>(bag).State = EntityState.Detached;
                _foodAngieContext.Entry<Nutrition>(bag.Nutrition).State = EntityState.Detached;
            }
            catch(Exception ex)
            {
                throw;
            }

        }
    }
}
