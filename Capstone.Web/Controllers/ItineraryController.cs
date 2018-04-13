using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class ItineraryController : ApiController
    {

		IItineraryDAL dal;

		public ItineraryController(IItineraryDAL dal)
		{
			this.dal = dal;
		}

		public ActionResult

		[Route("api/itinerary/")]
		[HttpPost]
		public IHttpActionResult SaveItinerary(ItineraryModel model)
		{



			// Calls the DAL to save the itinerary to the database
			return null;


		}

    }
}
