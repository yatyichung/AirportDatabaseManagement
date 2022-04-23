using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airport_Project_2022.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        public TimeSpan DepartureTime { get; set; }

        public string FlightStatus { get; set; }

        public string Airline { get; set; }

        public string FlightNumber { get; set; }

        public string Destination { get; set; }

        public int Terminals { get; set; }

        public int Gate { get; set; }

    }
}