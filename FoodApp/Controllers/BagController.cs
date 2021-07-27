using FoodDomain.Entities;
using FoodDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly IBagItemService _bagService;
        
        public BagController(IBagItemService bagService)
        {
            _bagService = bagService;
        }

        // GET: api/<BagItem>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/<FoodController>/5
        [HttpGet("{id}")]
        public async Task<BagItem> Get(string id)
        {
            var item = await _bagService.GetByName(id);
            return item;
        }

        // GET api/<FoodController>/5
        [HttpGet("userbags/{id}")]
        public async Task<ActionResult<List<BagItem>>> GetUserBags(string id)
        {
            int userId = Int32.Parse(id);
            var bags = await _bagService.GetBagsByUserId(userId);
            return bags.ToList();
        }

        // POST api/<BagItem>
        [HttpPost]
        public void Post([FromBody] BagItem value)
        {
            _bagService.Update(value);
        }

        // PUT api/<BagItem>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BagItem>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
