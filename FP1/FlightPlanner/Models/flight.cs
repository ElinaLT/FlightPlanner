using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flight_planner.Models
{
    public class flight
    {
        public int Id { get; set; }
        public airport From { get; set; }
        public airport To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public override bool Equals(object obj)
        {
            var flight = obj as flight;
            if (flight == null)
            {
                return false;
            }
            return flight.Carrier == Carrier && flight.ArrivalTime == ArrivalTime &&
                   flight.DepartureTime == DepartureTime && flight.From.Equals(From) &&
                   flight.To.Equals(To);
        }
    }
}