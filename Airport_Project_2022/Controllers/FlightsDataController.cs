using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Airport_Project_2022.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Airport_Project_2022.Controllers
{
    public class FlightsDataController : ApiController
    {

        // The database context class which allows us to access our MySQL Database.
        private AirportDbContext Airport = new AirportDbContext();

        //This Controller Will access the flights table of our airport database.
        /// <summary>
        /// Returns a list of flights in the system
        /// </summary>
        /// <param name="SearchKey">Search key (option) of flight number</param>
        /// <example>GET api/FlightsData/ListFlights</example>
        /// <example>GET api/FlightsData/ListFlights/AC3190</example>
        /// <returns>
        /// A list of flight objects 
        /// </returns>
        [HttpGet]
        [Route("api/FlightsData/ListFlights/{SearchKey?}")]
        public IEnumerable<Flight> ListFlights(string SearchKey = null)
        {
            if (SearchKey != null)
            {
                Debug.WriteLine("The search key is " + SearchKey);
            }

            //Create an instance of a connection
            MySqlConnection Conn = Airport.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select * from flights";

            if (SearchKey != null)
            {
                query = query + " where lower(flight_number) =lower(@key)";
                cmd.Parameters.AddWithValue("@key", SearchKey);
                cmd.Prepare();
            }

            Debug.WriteLine("The query is :" + query);

            cmd.CommandText = query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Author Names
            List<Flight> Flights = new List<Flight> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Flight NewFlight = new Flight();
                NewFlight.FlightId = Convert.ToInt32(ResultSet["flightid"]);
                NewFlight.DepartureTime = (TimeSpan)ResultSet["departure_time"];
                NewFlight.FlightStatus = ResultSet["flight_status"].ToString();
                NewFlight.Airline = ResultSet["airline"].ToString();
                NewFlight.FlightNumber = ResultSet["flight_number"].ToString();
                NewFlight.Destination = ResultSet["destination"].ToString();
                NewFlight.Terminals = Convert.ToInt32(ResultSet["terminals"]);
                NewFlight.Gate = Convert.ToInt32(ResultSet["gate"]);




                //Add the Author Name to the List
                Flights.Add(NewFlight);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Flights;
        }

        [HttpGet]
        [Route("api/flightdata/findflight/{flightid}")]
        public Flight FindFlight(int flightid)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Airport.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from flights where flightid=@id";
            cmd.Parameters.AddWithValue("@id", flightid);
            cmd.Prepare();



            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Flight Number
            Flight SelectedFlight = new Flight();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                SelectedFlight.FlightId = Convert.ToInt32(ResultSet["flightid"]);
                SelectedFlight.DepartureTime = (TimeSpan)ResultSet["departure_time"];
                SelectedFlight.FlightStatus = ResultSet["flight_status"].ToString();
                SelectedFlight.Airline = ResultSet["airline"].ToString();
                SelectedFlight.FlightNumber = ResultSet["flight_number"].ToString();
                SelectedFlight.Destination = ResultSet["destination"].ToString();
                SelectedFlight.Terminals = Convert.ToInt32(ResultSet["terminals"]);
                SelectedFlight.Gate = Convert.ToInt32(ResultSet["gate"]);


            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return SelectedFlight;
        }

        /// <summary>
        /// Add a new flight into the system given flight information
        /// <paramref name="NewFlight">The flight information being add</paramref>
        /// </summary>
        public void AddFlight(Flight NewFlight)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Airport.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();


            string query = "insert into flights (departure_time,flight_status,airline,flight_number,destination,terminals,gate) " +
                "values (@departure_time,@flight_status,@airline,@flight_number,@destination,@terminals,@gate)";
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@departure_time",NewFlight.DepartureTime);
            cmd.Parameters.AddWithValue("@flight_status", NewFlight.FlightStatus);
            cmd.Parameters.AddWithValue("@airline", NewFlight.Airline);
            cmd.Parameters.AddWithValue("@flight_number", NewFlight.FlightNumber);
            cmd.Parameters.AddWithValue("@destination", NewFlight.Destination);
            cmd.Parameters.AddWithValue("@terminals", NewFlight.Terminals);
            cmd.Parameters.AddWithValue("@gate", NewFlight.Gate);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }


    }
}
