using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace your_profile_asp_api.Controllers
{
    [Route("api/")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new AppResponse("ASP .NET Core API is running...", null,true));
        }
    }
}
