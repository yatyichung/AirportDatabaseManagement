using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airport_Project_2022.Models;
using System.Diagnostics;

namespace Airport_Project_2022.Controllers
{
    public class FlightController : Controller
    {
        // GET: Flight
        public ActionResult Index()
        {
            return View();
        }

        //GET: Flight/List
        //showing a page of all flights in the systems
        [Route("Flight/List/{SearchKey}")]
        public ActionResult List(string SearchKey)
        {
            //debugging message to see if we have gathered the key
            Debug.WriteLine("The key is " + SearchKey);

            //connect to our data access layer
            //get our flights
            //pass the flights to the view Flights/List.cshtml
            FlightsDataController controller = new FlightsDataController();
            IEnumerable<Flight> Flights = controller.ListFlights(SearchKey);


            return View(Flights);
        }

        //GET: /Flight/Show/{id}
        public ActionResult Show(int id)
        {
            FlightsDataController controller = new FlightsDataController();
            Flight SelectedFlight = controller.FindFlight(id);

            //route tyhe single flight info to view Flights/Show.cshtml

            return View(SelectedFlight);
        }

        //GET: /Flight/New
        public ActionResult New()
        {
            return View();
        }

        //POST: /Flight/Create
        [HttpPost]

        public ActionResult Create(TimeSpan departuretime,string flightstatus, string airline,string flightnumber, string destination, int terminal,int gate)
        {
            Debug.WriteLine("the flight info is: "+departuretime+" "+flightstatus+" "+airline+" "+flightnumber+" "+destination+" " +terminal+" "+ gate);
          
            Flight NewFlight = new Flight();
            NewFlight.DepartureTime = departuretime;
            NewFlight.FlightStatus = flightstatus;
            NewFlight.Airline = airline;
            NewFlight.FlightNumber = flightnumber;
            NewFlight.Destination = destination;
            NewFlight.Terminals = terminal;
            NewFlight.Gate = gate;

            FlightsDataController controller = new FlightsDataController();
            controller.AddFlight(NewFlight);

            //connect to a database
            //insert into flights with provided values

            //redirect immediately to the list view
            return RedirectToAction("List");
        }

        //GET: /Flight/DeleteConfirm/{id}
        //[Route("/Flight/DeleteConfirm/{FlightId}")]
        public ActionResult DeleteConfirm(int id)
        {
            FlightsDataController controller = new FlightsDataController();
            Flight SelectedFlight = controller.FindFlight(id);

            //route tyhe single flight info to view Flights/Show.cshtml

            return View(SelectedFlight);
        }

        //POST: /Flight/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            FlightsDataController controller = new FlightsDataController();

            controller.DeleteFlight(id);

            return RedirectToAction("List");
        }


        //GET: /Flight/Edit/{id}
        public ActionResult Edit(int id)
        {
            //pass flight info to the view to show that to the user

            FlightsDataController controller = new FlightsDataController();

            Flight SelectedFlight = controller.FindFlight(id);

            return View(SelectedFlight);
        }

        //POST: /Flight/Update/{id}
       /// <summary>
       ///  This method actually updates the flight
       /// </summary>
       /// <param name="id"></param>
       /// <param name="departuretime"></param>
       /// <param name="flightstatus"></param>
       /// <param name="airline"></param>
       /// <param name="flightnumber"></param>
       /// <param name="destination"></param>
       /// <param name="terminal"></param>
       /// <param name="gate"></param>
       /// <returns></returns>
        [HttpPost]
        public ActionResult Update(int id, TimeSpan departuretime, string flightstatus, string airline, string flightnumber, string destination, int terminal, int gate)
        {
            Flight FlightInfo = new Flight();

            FlightInfo.DepartureTime = departuretime;
            FlightInfo.FlightStatus = flightstatus;
            FlightInfo.Airline = airline;
            FlightInfo.FlightNumber = flightnumber;
            FlightInfo.Destination = destination;
            FlightInfo.Terminals = terminal;
            FlightInfo.Gate = gate;
            

            //update the flight information
            FlightsDataController controller = new FlightsDataController();
            controller.UpdateFlight(id, FlightInfo);

            //return to the Flight that I just changed
            return RedirectToAction("Show/"+id);
        }



    }
}