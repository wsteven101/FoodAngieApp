using FoodDomain.DTO.Repo;
using FoodDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomain.Interfaces
{
    public interface IUserService
    {
        public Task<UserEntity> GetByUserId(string userName);
        public Task Update(UserEntity user);
    }
}
