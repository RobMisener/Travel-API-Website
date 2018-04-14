using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
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

        // GET: Survey
        public ActionResult Itinerary()
        {
            List<ItineraryModel> itineraryModelList = dal.GetItinerary();

            return View(itineraryModelList);
        }

        [Route("api/itinerary/")]
		[HttpPost]
		public IHttpActionResult Itinerary(ItineraryModel model)
		{

            bool result = dal.CreateItinerary(model);
            return RedirectToAction("Itinerary");

		}


    }
}
