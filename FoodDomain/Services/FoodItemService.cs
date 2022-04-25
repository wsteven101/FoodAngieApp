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
    public class FoodItemService : IFoodItemService
    {
        private readonly IMapper _mapper;
        private IFoodItemRepo _foodRepo;


        public FoodItemService(IFoodItemRepo foodRepo, IMapper mapper)
        {
            _foodRepo = foodRepo;
            _mapper = mapper;
        }


        public FoodEntity Get(long id)
        {
            //foreach(var f in _mockDBList)
            //{
            //    if (f.Value.Id == id)
            //    {
            //        return f.Value;
            //    }
            //}
            return default;
        }

        public FoodEntity GetByName(string name)
        {
            //if (_mockDBList.TryGetValue(name, out FoodItem item))
            //{
            //    return item;
            //}

            return default;
        }

        public async Task<List<FoodEntity>> GetUserFoods(long userId)
        {
            var userItemDtos = (await _foodRepo.GetFoodsByUserId(userId)).ToList();
            var userItems = _mapper.Map<List<FoodEntity>>(userItemDtos);
            return userItems.ToList();
        }


        public void Update(FoodEntity foodItem)
        {

            //if ( _mockDBList.TryGetValue(foodItem.Name, out FoodItem updateItem) )
            //{
            //    _mockDBList.Remove(foodItem.Name);
            //    foodItem.Id = GetNextId();
            //}
            //_mockDBList[foodItem.Name] = foodItem;

        }
    }
}
