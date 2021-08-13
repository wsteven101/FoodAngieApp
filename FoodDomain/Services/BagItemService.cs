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


        public async Task<BagItem> GetByName(string name)
        {
            var bagDto = await _bagRepo.GetByName(name);
            var bagItem = _mapper.Map<BagItem>(bagDto);

            await FillBag(bagItem);

            return bagItem;
        }

        public async Task<List<BagItem>> GetBagsByUserId(long userId)
        {
            var userBagsDtos = (await _bagRepo.GetBagsByUserId(userId)).ToList();
            var userBagItems = _mapper.Map<List<BagItem>>(userBagsDtos);
            return userBagItems.ToList();
        }

        public async Task FillBag(BagItem bag)
        {
            List<long> bagIdList = new List<long>();
            List<long> foodIdList = new List<long>();

            AddToBagList(bag, bagIdList, foodIdList);

            var bagDtos = await _bagRepo.GetBags(bagIdList);
            var foodDtos = await _foodRepo.GetFoods(foodIdList);

            var bags = _mapper.Map<List<BagItem>>(bagDtos);
            var foods = _mapper.Map<List<FoodItem>>(foodDtos);

            ReplacePlaceholderItems(bag, bags, foods);
        }

        private void AddToBagList(
            BagItem parentBag, 
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
            BagItem parentBag, 
            List<BagItem> bagList,
            List<FoodItem> foodList)
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

        private bool isRefreshFood(FoodItem food) => (food.UpdateData == true) || (food.Nutrition == null);

        private bool isRefreshBag(BagItem bag) => (bag.UpdateData == true) || (bag.Nutrition == null);

        public async Task Update(BagItem bag)
        {
            try
            {
                bag.Nutrition.Id = bag.Id;
                var bagDto = _mapper.Map<BagRDto>(bag);
                await _bagRepo.Update(bagDto);
            }
            catch(Exception ex)
            {
                throw;
            }

        }
    }
}

