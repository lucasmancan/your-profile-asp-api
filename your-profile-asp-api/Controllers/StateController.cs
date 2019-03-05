using aspApi.Models;
using aspApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace aspApi.Controllers
{
    [Route("states/")]
    public class StateController : Controller
    {
        private readonly IStateRepository _stateRepository;

        public StateController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        [Route("country/")]
        [HttpGet("country/{id}")]
        public ActionResult GetByCountry(int id)
        {

            try
            {
                var states = _stateRepository.findByContry(id);
                return new ObjectResult(new AppResponse("", states, true));
            }
            catch (Exception e)
            {
                return StatusCode(500, new AppResponse("Oops... an error ocurred", e, false));
            }
        }

    }
}
