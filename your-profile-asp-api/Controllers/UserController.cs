using aspApi.Models;
using aspApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using aspApi.Services;
using Microsoft.Extensions.Options;

namespace aspApi.Controllers
{

    [Route("users/")]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly MessageSender messageSender;

        public UserController(IUserRepository userRepository, IOptions<EmailSettings> options)
        {
            _userRepository = userRepository;
            messageSender = new MessageSender(options);
        }

        [HttpGet(Name = "GetUser"), Authorize]
        public IActionResult GetLoggedUser()
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = _userRepository.Find(int.Parse(userId.ToString()));

                if (user == null) return NotFound();

                return new ObjectResult(new AppResponse("Welcome to your profile!", user, true));

            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops.. An Error Occurred!", e, true));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] User user)
        {
            try
            {
                if (user == null) return BadRequest();

                var _user = _userRepository.FindByEmail(user.Email);

                if (_user != null)
                    return new ObjectResult(new AppResponse("User already exists", null, false));


                user = _userRepository.Add(user);

                messageSender.SendEmailAsync(user.Email, "Welcome " + user.Email, "Welcome to your new profile online");

                return new ObjectResult(new AppResponse("Welcome to Your Profile", _userRepository.createToken(user), true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops.. An Error Occurred!", e, true));
            }
        }



        [HttpPost("coverImage/{id}")]
        [Route("coverImage/")]
        [Authorize]
        public IActionResult UploadProfileImage([FromBody] string image, int id)
        {
            try
            {
                if (image == null) return BadRequest();

                _userRepository.
                   uploadProfileImage(id, image);

                return new ObjectResult(new AppResponse("Welcome to Your Profile", null, true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops.. An Error Occurred!", e, true));
            }
        }

        [HttpPost("updatePassword/")]
        [Route("updatePassword/")]
        public ActionResult ResetPassword([FromBody] string email)
        {
            try
            {
                var user = _userRepository.FindByEmail(email.Trim());

                if (user == null) return new ObjectResult(new AppResponse("You are not a user yet.", null, false));

                Random rnd = new Random();
                var pass =  rnd.Next(5555, 13454).ToString();

                user.Password = pass;

                _userRepository.UpdatePassword(user);

                messageSender.SendEmailAsync(user.Email, "New password", "Your new Password: " + pass);
                return new ObjectResult(new AppResponse("A new Password was sent to your email.", null, true));

            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops.. An Error Occurred!", e, true));
            }
        }
        [HttpPost("profileImage/{id}")]
        [Route("profileImage/")]
        [Authorize]
        public async Task<IActionResult> UploadCoverImage([FromBody] string image, int id)
        {
            try
            {
                if (image == null) return BadRequest();

                string convert = image.Replace("data:image/png;base64,", String.Empty);
                convert = image.Replace("data:image/jpeg;base64,", String.Empty);

                //byte[] image64 = Convert.FromBase64String(convert);
                await _userRepository.uploadProfileImage(id, convert);

                return new ObjectResult(new AppResponse("Welcome to Your Profile", null, true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops.. An Error Occurred!", e, true));
            }
        }


        [HttpPut, Authorize]
        public IActionResult Update([FromBody] User user)
        {
            try
            {
                if (user == null) return BadRequest();

                _userRepository.Update(user);

                return new ObjectResult(new AppResponse("Profile Updated!", null, true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops.. An Error Occurred!", e, true));
            }
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var _user = _userRepository.Find(id);
                if (_user == null) return NotFound();
                _userRepository.Remove(id);

                return new ObjectResult(new AppResponse("User Deleted!", null, true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops.. An Error Occurred!", e, true));
            }
        }
    }
}
