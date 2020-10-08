using flight_planner.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace flight_planner.Controllers
{
    public class ClientApiController : ApiController
    {

        [HttpGet]
        [Route("api/flights/{id}")] 
        public HttpResponseMessage GetFlightById(HttpRequestMessage request, int id) 
        {

            var flight = FlightStorage.GetFlightById(id); 
            if (flight == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound); 
            }
            return request.CreateResponse(HttpStatusCode.OK, flight);
        }

        [HttpGet]
        [Route("api/airports")] 
        public List<airport> GetAirport(string search)
        {
            return FlightStorage.GetAirport(search);
        }

        [HttpPost]
        [Route("api/flights/search")]
        public HttpResponseMessage FlightSearch(HttpRequestMessage request, FlightSearch flight)
        {
            if (!IsValid(flight))
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var flights = FlightStorage.GetFlights(flight.From, flight.To, flight.DepartureDate);
            var response = new FlightSearchResult(flights);

            return request.CreateResponse(HttpStatusCode.OK, response);
        }

        private static bool IsValid(FlightSearch flightSearch) 
        {
            if (flightSearch != null &&
                flightSearch.From != null &&
                flightSearch.To != null &&
                flightSearch.DepartureDate.GetHashCode() != 0 &&
                flightSearch.From != flightSearch.To)
            {
                return true;
            }
            return false;
        }
    }
}