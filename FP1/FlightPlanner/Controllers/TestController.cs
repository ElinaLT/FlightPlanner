using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using flight_planner.Models;

namespace flight_planner.Controllers
{
    public class TestApiController : ApiController
    {
        [HttpPost]
        [Route("testing-api/clear")]
        public string Clear()
        {
            FlightStorage.ClearFlights();
            return "OK";
        }

        [HttpGet]
        [Route("testing-api/")]
        public string Get()
        {
            return "Testing api";
        }
    }
}