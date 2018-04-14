using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using Capstone.Web.Models;
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

        // GET: Itinerary
        public ActionResult GetItinerary()
        {
            List<ItineraryModel> itineraryModelList = dal.GetItinerary();

            return Ok(itineraryModelList);
        }

        //POST: Create Itinerary
        [HttpPost]
        [Route("api/itinerary/")]
		public IHttpActionResult CreateItinerary(ItineraryModel model)
		{

            bool result = dal.CreateItinerary(model);
            return RedirectToAction("Itinerary");

		}
        //POST: Update Itinerary
        [HttpPost]
        [Route("api/itinerary/")]
        public IHttpActionResult UpdateItinerary(ItineraryModel model)
        {

            bool result = dal.UpdateItinerary(model);
            return RedirectToAction("Itinerary");

        }
        //DELETE: Delete Itinerary
        [HttpDelete]
        [Route("api/itinerary/")]
        public IHttpActionResult DeleteItinerary(ItineraryModel model)
        {

            bool result = dal.DeleteItinerary(model);
            return RedirectToAction("Itinerary");

        }


    }
}
