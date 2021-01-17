using FoodDomain.Entities;
using FoodDomain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDomain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {

        readonly IFoodItemService foodService;

        public FoodController(IFoodItemService foodItemService)
        {
            foodService = foodItemService;
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
        public FoodItem Get(string id)
        {
            var food = foodService.GetByName(id);
            return food;
        }

        // POST api/<FoodController>
        [HttpPost]
        public void Post([FromBody] FoodItem foodItem)
        {
            foodService.Update(foodItem);
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
