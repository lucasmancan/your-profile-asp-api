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
using aspApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace aspApi.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly MessageSender messageSender;
        private readonly IOptions<EmailSettings> _options;
        public AuthController(IUserRepository userRepository, IOptions<EmailSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
            messageSender = new MessageSender(options);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Credentials credentials)
        {
            var token = _userRepository.Authenticate(credentials);

            if(token == null) return BadRequest(new AppResponse("Username or password may be incorrect!", null, true));

            messageSender.SendEmailAsync(credentials.email, "Welcome again " + credentials.email, "Welcome to your new profile online");

            return Ok(new AppResponse("Welcome to your profile!", _userRepository.Authenticate(credentials), true));
        }


    }
}
