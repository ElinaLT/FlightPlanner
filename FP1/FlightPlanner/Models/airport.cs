using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flight_planner.Models
{
    public class airport
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Airport { get; set; }

        public airport(string country, string city, string airport)
        {
            Country = country;
            City = city;
            Airport = airport;
        }
        public override bool Equals(object obj) 
        {
            var airport = obj as airport;
            if (airport == null)
            {
                return false;
            }

            return string.Equals(airport.Airport.Trim(), Airport.Trim(), StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(airport.Country.Trim(), Country.Trim(), StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(airport.City.Trim(), City.Trim(), StringComparison.OrdinalIgnoreCase);
        }
        public bool Contains(string substring, StringComparison comp)
        {
            if (substring == null)
                throw new ArgumentNullException(nameof(substring), "substring cannot be null.");

            if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("comp is not a member of StringComparison", nameof(comp));

            return Country.IndexOf(substring.Trim(), comp) >= 0 || 
                   City.IndexOf(substring.Trim(), comp) >= 0 ||
                   Airport.IndexOf(substring.Trim(), comp) >= 0;
        }
    }
}
