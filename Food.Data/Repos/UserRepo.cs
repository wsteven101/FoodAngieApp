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
    public class UserRepo: IUserRepo
    {
        private FoodAngieContext _foodAngieContext;
        private readonly IMapper _mapper;

        public UserRepo(FoodAngieContext foodAngieContext,
                IMapper mapper)
        {
            _foodAngieContext = foodAngieContext;
            _mapper = mapper;
        }

        public async Task<UserRDto> GetByUserId(string userName)
        {
            var user = await _foodAngieContext.Users
                .Where(u => u.UserName == userName)
                .SingleOrDefaultAsync();

            return _mapper.Map<UserRDto>(user);
        }

        public async Task Update(UserRDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            _foodAngieContext.Entry<User>(user).CurrentValues.SetValues(user);
            await _foodAngieContext.SaveChangesAsync();

            // detach entities so that update can be applied more than once by integration tests
            var entry = _foodAngieContext.Entry<User>(user).State = EntityState.Detached; ;
        }
    }
}
