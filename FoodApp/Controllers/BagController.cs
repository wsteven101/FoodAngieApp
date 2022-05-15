using AutoMapper;
using FoodApp.DTO;
using FoodDomain.Entities;
using FoodDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;
        private readonly IBagItemService _bagService;
        
        public BagController(IBagItemService bagService,
            IMapper mapper)
        {
            _mapper = mapper;
            _bagService = bagService;
        }

        // GET: api/<BagItem>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/<FoodController>/5
        [HttpGet("{name}")]
        public async Task<BagEntity> Get(string name)
        {
            var item = await _bagService.GetByName(name);
            return item;
        }

        // GET api/<FoodController>/5
        /// <summary>
        /// Returns all the bags owned by a specific user
        /// </summary>
        /// <param name="id">User Id (not user name)</param>
        /// <returns></returns>
        [HttpGet("userbags/{id}")]
        public async Task<ActionResult<List<BagItemADto>>> GetUserBags(string id)
        {
            try
            {
                int userId = Int32.Parse(id);
                var bags = (await _bagService.GetBagsByUserId(userId)).ToList();

                var bagDtos =_mapper.Map<List<BagItemADto>>(bags);
                return bagDtos;
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("fillbag")]
        public async Task<ActionResult<BagItemADto>> FillBag([FromBody] BagItemADto bagDto)
        {
            try
            {
                var bag =_mapper.Map<BagEntity>(bagDto);
                await _bagService.FillBag(bag);

                var filledBagDto = _mapper.Map<BagItemADto>(bag);
                return filledBagDto;
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        // POST api/<BagItem>
        [HttpPost]
        public void Post([FromBody] BagEntity value)
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
