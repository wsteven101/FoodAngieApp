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
    }
}
