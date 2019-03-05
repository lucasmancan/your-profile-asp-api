using aspApi.Models;
using aspApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace aspApi.Controllers
{
    [Route("cities/")]
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [Route("state/")]
        [HttpGet("state/{id}")]
        public ActionResult GetByState (int id)
        {

            try
            {
                var cities = _cityRepository.findByState(id);
                return new ObjectResult(new AppResponse("", cities, true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops... an error ocurred", e, false));
            }
        }

    }
}
