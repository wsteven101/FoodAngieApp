using FoodDomain.DTO.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Repositories
{
    public interface IUserRepo
    {
        public Task<UserRDto> GetByUserId(string userName);
        public Task Update(UserRDto userDto);
    }
}
