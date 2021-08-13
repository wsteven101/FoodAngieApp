using AutoMapper;
using FoodApp.DTO;
using FoodApp.Interfaces;
using FoodDomain.Entities;
using FoodDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace FoodApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserService _userService;
        private readonly IKeyStore _keyStore;

        public AuthenticationController(
            IConfiguration configuration,
            ITokenService tokenService,
            IEncryptionService encryptionService,
            IUserService userService,
            IKeyStore keyStore)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _encryptionService = encryptionService;
            _userService = userService;
            _keyStore = keyStore;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserADto userDto)
        {
            if (string.IsNullOrEmpty(userDto.UserName))
            {
                return new UnauthorizedResult();
            }

            var passwordKey = "PasswordKey";

            //var passwordEncrypted = _encryptionService.EncryptString(userDto.Password, passwordKey);

            var user = await _userService.GetByUserId(userDto.UserName);
            if (user == default)
            {
                return new UnauthorizedResult();
            }

            var password = _encryptionService.DecryptString(user.Password, passwordKey);
            if (userDto.Password != password)
            {
                return new UnauthorizedResult();
            }

            //var validUser = new UserADto { Id = 101, UserName = "anon", FirstName = "Auth", Surname = "Approver" };

            var jwtHashKey = _keyStore.GetKey("JwtKey");
            var generatedToken = _tokenService.BuildToken(
                jwtHashKey,
                _configuration["Jwt:Issuer"].ToString(),
                user
                );

            return new OkObjectResult( generatedToken);

            //if (generatedToken != null)
            //{
                
            //    HttpContext.Session.SetString("Token", generatedToken);
            //}
        }
    }
}
