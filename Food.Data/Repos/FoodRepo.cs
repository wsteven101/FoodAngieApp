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
    public class FoodRepo : IFoodItemRepo
    {
        private FoodAngieContext _foodAngieContext;
        private readonly IMapper _mapper;

        public FoodRepo(FoodAngieContext foodAngieContext,
                IMapper mapper)
        {
            _foodAngieContext = foodAngieContext;
            _mapper = mapper;
        }

        public FoodRDto GetByName(string name)
        {
            var food = _foodAngieContext.Foods
                .Include(f => f.Nutrition)
                .Where(f => f.Name == name)
                .FirstOrDefault();

            var foodDto = _mapper.Map<FoodRDto>(food);

            return foodDto;
        }

        public async Task<List<FoodRDto>> GetFoodsByUserId(long userId)
        {
            var items = await _foodAngieContext.Foods
                .Include(b => b.Nutrition)
                .Where(b => b.UserId == userId)
                .Join(
                    _foodAngieContext.Users,
                    b => b.UserId,
                    u => u.Id,
                    (b, u) => b
                )
                .ToListAsync();

            var userItemDtos = _mapper.Map<List<FoodRDto>>(items);

            return userItemDtos;
        }

        public async Task<List<FoodRDto>> GetFoods(List<long> foodList)
        {
            var bags = await _foodAngieContext.Foods
                .Include(b => b.Nutrition)
                .Where(b => foodList.Contains(b.Id))
                .ToListAsync();

            var foodDtos = _mapper.Map<List<FoodRDto>>(bags);
            return foodDtos;
        }

    }
}
