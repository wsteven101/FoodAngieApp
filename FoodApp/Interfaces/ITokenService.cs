using FoodApp.DTO;
using FoodDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodApp.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(
            string key,
            string issuer,
            UserEntity user);
        bool ValidateToken(
            string key,
            string issuer,
            string audience,
            string token);
            
    }
}
