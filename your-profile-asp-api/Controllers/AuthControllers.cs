using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using aspApi.Models;
using aspApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace aspApi.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {

        private readonly IConfiguration _configuration;
        private AppResponse appResponse;
        private readonly IUserRepository _userRepository;


        public AuthController(IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Credentials credentials)
        {

            var user = _userRepository.Authenticate(credentials.email);

            if (user == null || (credentials.password != user.password))
                return BadRequest(new AppResponse("Username or password may be incorrect!", null, true));

        
            return Ok(new AppResponse("Welcome to your profile!", createToken(user), true));
        }


   

        bool ValidatePassword(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return (0 == comparer.Compare(hashOfInput, hash)) ? true : false;
        }


        string GetMd5Hash(string input)
        {

            using (MD5 encoder = MD5.Create())
            {

                byte[] data = encoder.ComputeHash(Encoding.UTF8.GetBytes(input + _configuration["SecurityKey"]));
                StringBuilder value = new StringBuilder();

                for (long i = 0; i < data.Length; i++)
                {
                    value.Append(data[i].ToString());
                }

                return value.ToString();
            }

        }

    }
}
