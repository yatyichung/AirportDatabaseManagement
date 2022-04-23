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

        public ActionResult Create(string departuretime,string flightstatus, string airline,string flightnumber, string terminal,string gate)
        {
            Debug.WriteLine("the flight info is: "+departuretime+" "+flightstatus+" "+airline+" "+flightnumber+" "+terminal+" "+ gate);
          
            //connect to a database
            //insert into flights with provided values
            
            //redirect immediately to the list view
            return RedirectToAction("List");
        }

    }
}