using FoodDomain.Entities;
using FoodDomain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDomain.Interfaces;
using AutoMapper;
using FoodApp.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IMapper _mapper;
        readonly IFoodItemService _foodService;

        public FoodController(
            IFoodItemService foodItemService,
            IMapper mapper)
        {
            _mapper = mapper;
            _foodService = foodItemService;
        }

        // GET: api/<FoodController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FoodController>/5
        // e.g. https://localhost:5001/api/Food/xx
        [HttpGet("{id}")]
        public FoodEntity Get(string id)
        {
            var food = _foodService.GetByName(id);
            return food;
        }

        [HttpGet("GetUserFoods/{id}")]
        public async Task<List<FoodItemADto>> GetUserFoods(string id)
        {
            int userId = Int32.Parse(id);
            var foods = await _foodService.GetUserFoods(userId);

            var foodDtos = _mapper.Map<List<FoodItemADto>>(foods);
            return foodDtos;
        }

        // POST api/<FoodController>
        [HttpPost]
        public void Post([FromBody] FoodEntity foodItem)
        {
            _foodService.Update(foodItem);
        }

        // PUT api/<FoodController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FoodController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
