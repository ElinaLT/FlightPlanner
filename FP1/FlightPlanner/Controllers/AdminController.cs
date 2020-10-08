using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using flight_planner.Models;
using FlightPlanner.Attributes;

namespace flight_planner.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : ApiController

    {
        [HttpGet]
        [Route("admin-api/flights/{id}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id) 
        {
            var flight = FlightStorage.GetFlightById(id); 
            if (flight == null) 
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, flight); 
        }

        [HttpPut]
        [Route("admin-api/flights")]
        public HttpResponseMessage AddFlight(HttpRequestMessage request, flight flight)
        {
            if (!IsValid(flight)) 
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, flight);
            }
            
            flight.Id = FlightStorage.GetNextId();

            if (!FlightStorage.AddFlight(flight))
            {
                return request.CreateResponse(HttpStatusCode.Conflict, flight);
            }
            return request.CreateResponse(HttpStatusCode.Created, flight); 
        }
        private bool IsValid(flight flight)
        {
            return
                !string.IsNullOrEmpty(flight.ArrivalTime) &&
                !string.IsNullOrEmpty(flight.DepartureTime) &&
                !string.IsNullOrEmpty(flight.Carrier) &&
                !string.IsNullOrEmpty(flight.DepartureTime) &&
                IsValidAirport(flight.From) && IsValidAirport(flight.To) &&
                !flight.From.Equals(flight.To) &&
                IsValidDateTime(flight.DepartureTime, flight.ArrivalTime);
        }
        
        private bool IsValidAirport(airport airport)
        {
            return airport != null && 
                   !string.IsNullOrEmpty(airport.Airport) &&
                   !string.IsNullOrEmpty(airport.City) &&
                   !string.IsNullOrEmpty(airport.Country);
        }
        
        private static bool IsValidDateTime(string departTime, string arrTime)
        {
            if (!string.IsNullOrEmpty(departTime) && !string.IsNullOrEmpty(arrTime))
            {
                DateTime dTime = DateTime.Parse(departTime);
                DateTime aTime = DateTime.Parse(arrTime);
                return DateTime.Compare(aTime, dTime) > 0;
            }
                return false;
        }

        [HttpDelete]
        [Route("admin-api/flights/{id}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id) 
        {
            FlightStorage.RemoveFlightById(id); 
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}