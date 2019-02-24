using aspApi.Models;
using aspApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Controllers
{

[Route("users/")]
public class UserController : Controller
{

    private readonly IUserRepository _userRepository;
    private AppResponse appResponse;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IEnumerable<User> GetAll()
    {
        return _userRepository.GetAll();
    }

    [HttpGet("{id}", Name = "GetUser")]
    public IActionResult GetById(int id)
    {
        var user = _userRepository.Find(id);


        if (user == null)
        {
            return NotFound();
        }

        appResponse = new AppResponse("Welcome to your profile!", user, true);

        return new ObjectResult(appResponse);
    }

    [HttpPost]
    public IActionResult Create([FromBody] User user) {

        if (user == null) return BadRequest();

        _userRepository.Add(user);

        appResponse = new AppResponse("User created", null, true);

        return new ObjectResult(appResponse);

    }

    [HttpPut]
    public IActionResult Update([FromBody] User user)
    {

       


        try{

                var _user = _userRepository.Find(user.id);
                if (_user == null) return NotFound();

                _user.firstName = user.firstName;
                _user.lastName = user.lastName;
                _user.gender = user.gender;
                _user.BirthDate = user.BirthDate;
                _user.updatedAt = DateTimeOffset.Now;
                _user.address = user.address;
                _userRepository.UpdateAsync(_user);

        appResponse = new AppResponse("Profile Updated!", null, true);
        return new ObjectResult(appResponse);

        }catch(Exception e){
        appResponse = new AppResponse("Oops.. An Error Occurred!", e, true);
        return StatusCode(500);
        }


    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {

        var _user = _userRepository.Find(id);


        if (_user == null) return NotFound();

        _userRepository.Remove(id);

        appResponse = new AppResponse("User Deleted!", null, true);

        return new ObjectResult(appResponse);

    }
}
}
