using aspApi.Models;
using aspApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace aspApi.Controllers
{
    [Route("countries/")]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository CountryRepository)
        {
            _countryRepository = CountryRepository;
        }
        public ActionResult GetByCountry()
        {

            try
            {
                var countries = _countryRepository.GetAll();
                return new ObjectResult(new AppResponse("", countries, true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops... an error ocurred", e, false));
            }
        }

    }
}
