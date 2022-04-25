using AutoMapper;
using FoodDomain.DTO.Repo;
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
        private IFoodItemRepo _foodRepo;

        public BagItemService(
            IBagItemRepo bagRepo, 
            IFoodItemRepo foodRepo,
            IMapper mapper)
        {
            _bagRepo = bagRepo;
            _foodRepo = foodRepo;
            _mapper = mapper;
        }


        public async Task<BagEntity> GetByName(string name)
        {
            var bagDto = await _bagRepo.GetByName(name);
            var bagItem = _mapper.Map<BagEntity>(bagDto);

            await FillBag(bagItem);

            return bagItem;
        }

        public async Task<List<BagEntity>> GetBagsByUserId(long userId)
        {
            var userBagsDtos = (await _bagRepo.GetBagsByUserId(userId)).ToList();
            var userBagItems = _mapper.Map<List<BagEntity>>(userBagsDtos);
            return userBagItems.ToList();
        }

        public async Task FillBag(BagEntity bag)
        {
            List<long> bagIdList = new List<long>();
            List<long> foodIdList = new List<long>();

            AddToBagList(bag, bagIdList, foodIdList);

            var bagDtos = await _bagRepo.GetBags(bagIdList);
            var foodDtos = await _foodRepo.GetFoods(foodIdList);

            var bags = _mapper.Map<List<BagEntity>>(bagDtos);
            var foods = _mapper.Map<List<FoodEntity>>(foodDtos);

            ReplacePlaceholderItems(bag, bags, foods);
        }

        private void AddToBagList(
            BagEntity parentBag, 
            List<long> bagIdList,
            List<long> foodIdList)
        {

            if (isRefreshBag(parentBag))
            {
                bagIdList.Add(parentBag.Id);
            }

            foreach (var foodNode in parentBag.Foods)
            {
                if (isRefreshFood(foodNode.Food))
                {
                    foodIdList.Add(foodNode.Food.Id);
                }
            }
             
            foreach (var bagNode in parentBag.Bags)
            {
                AddToBagList(bagNode.Bag, bagIdList, foodIdList);
            }

        }

        public void ReplacePlaceholderItems(
            BagEntity parentBag, 
            List<BagEntity> bagList,
            List<FoodEntity> foodList)
        {

            var placeholderFoods = parentBag.Foods
                .Where(f => isRefreshFood(f.Food))
                .ToList();

            var placeholderBags = parentBag.Bags
                .Where(b => isRefreshBag(b.Bag))
                .ToList();

            foreach(var placeholderFood in placeholderFoods)
            {
                parentBag.Foods.Remove(placeholderFood);
                var food = foodList.FirstOrDefault(f => f.Id == placeholderFood.Food.Id);
                if (food != default)
                {
                    parentBag.Foods.Add(placeholderFood with { Food = food });
                }
            }

            foreach (var placeholderBag in placeholderBags)
            {
                parentBag.Bags.Remove(placeholderBag);
                var bag = bagList.FirstOrDefault(f => f.Id == placeholderBag.Bag.Id);
                if (bag != default)
                {
                    parentBag.Bags.Add(placeholderBag with { Bag = bag });
                }
            }

        }

        private bool isRefreshFood(FoodEntity food) => (food.UpdateData == true) || (food.Nutrition == null);

        private bool isRefreshBag(BagEntity bag) => (bag.UpdateData == true) || (bag.Nutrition == null);

        public async Task Update(BagEntity bag)
        {
            try
            {
                bag.Nutrition.Id = bag.Id;
                var bagDto = _mapper.Map<BagEntity>(bag);
                await _bagRepo.Update(bagDto);
            }
            catch(Exception ex)
            {
                throw;
            }

        }
    }
}

