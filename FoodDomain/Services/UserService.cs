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
    public class UserService: IUserService
    {

        private readonly IMapper _mapper;
        private IUserRepo _userRepo;

        public UserService(
            IUserRepo userRepo,
            IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserEntity> GetByUserId(string userId)
        {
            try
            {
                var userDto = await _userRepo.GetByUserId(userId);
                var user = _mapper.Map<UserEntity>(userDto);
                return user;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task Update(UserEntity user)
        {
            try
            {
                var userDto = _mapper.Map<UserRDto>(user);
                await _userRepo.Update(userDto);
            }
            catch(Exception ex)
            {
                throw;
            }

        }

    }
}
