using System;
using System.Collections.Generic;
using System.Linq;

namespace flight_planner.Models
{
    public class FlightStorage
    {
        private static SynchronizedCollection<flight> _flights { get; set; }
        private static int _id;
        private static readonly object ListLock = new object();
        static FlightStorage()
        {
            _flights = new SynchronizedCollection<flight>();
            _id = 1;
        }
        public static bool AddFlight(flight flight)
        {
            lock (ListLock)
            {
                if (!_flights.Any(f => f.Equals(flight)))
                {
                    _flights.Add(flight);
                    return true;
                }
                return false;
            }
            
        }
        public static flight GetFlightById(int id)
        {
            lock (ListLock)
            {
                var pair = _flights.FirstOrDefault(f => f.Id == id);
                return pair;
            }
        }
        public static List<flight> GetFlights(string from, string to, DateTime departureDate)
        {
            var result = new List<flight>();
            foreach (var flight in _flights)
            {
                DateTime flightDate = DateTime.Parse(flight.DepartureTime);
                if (flight.From.Airport == from && flight.To.Airport == to && flightDate.Date == departureDate.Date)
                {
                    result.Add(flight);
                }
            }
            return result;
        }
        public static int GetNextId()
        {
            return _id++;
        }
        public static List<airport> GetAirport(string search)
        {
            StringComparison comp = StringComparison.OrdinalIgnoreCase;
            var airports = new List<airport>();
            foreach (var keyValuePair in _flights)
            {

                if (keyValuePair.From.Contains(search, comp))
                {
                    if (!airports.Contains(keyValuePair.From))
                        airports.Add(keyValuePair.From);
                }

                if (keyValuePair.To.Contains(search, comp))
                {
                    if (!airports.Contains(keyValuePair.To))
                        airports.Add(keyValuePair.To);
                }
            }
            return airports;
        }
        public static void ClearFlights()
        {
            _flights.Clear();
        }

        public static bool RemoveFlightById(int id)
        {
            {
                var flight = GetFlightById(id);

                if (flight == null) return false;
                _flights.Remove(flight);
                return true;
            }
        }
    }
}