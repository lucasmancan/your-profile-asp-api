using aspApi.Models;
using aspApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace aspApi.Controllers
{
    [Route("phones/")]
    public class PhoneController : Controller
    {
        private readonly IPhoneRepository _phoneRepository;

        public PhoneController(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] Phone phone)
        {
            try
            {
                if (phone == null) return BadRequest();

                _phoneRepository.Add(phone);
                return new ObjectResult(new AppResponse("Phone Created", null, true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops.. An Error Occurred!", e, true));
            }
        }

        [HttpPut, Authorize]
        public IActionResult Update([FromBody] Phone phone)
        {
            try
            {
                var _phone = _phoneRepository.Find(phone.Id);
                if (_phone == null) return NotFound();

                _phone.PhoneNumber = phone.PhoneNumber;
                _phone.DDD = phone.DDD;
                _phone.DDI = phone.DDI;
                _phone.UpdatedAt = DateTimeOffset.Now;

                return new ObjectResult(new AppResponse("Phone Updated!", null, true));
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
                var _phone = _phoneRepository.Find(id);
                if (_phone == null) return NotFound();
                _phoneRepository.Remove(id);

                return new ObjectResult(new AppResponse("Phone Deleted!", null, true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops.. An Error Occurred!", e, true));
            }
        }
    }
}
