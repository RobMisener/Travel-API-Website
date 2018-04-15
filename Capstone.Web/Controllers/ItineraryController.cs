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

            return View(itineraryModelList);
        }


        //POST: Create Itinerary
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/itinerary/")]
		public IHttpActionResult CreateItinerary(ItineraryModel model)
		{

            bool result = dal.CreateItinerary(model);

            return RedirectToAction("_LoginPartial");

		}
        //POST: Update Itinerary
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/itinerary/")]
        public IHttpActionResult UpdateItinerary(ItineraryModel model)
        {

            bool result = dal.UpdateItinerary(model);
            return RedirectToAction("_LoginPartial");

        }
        //DELETE: Delete Itinerary
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/itinerary/{ItinId}")]
        public IHttpActionResult DeleteItinerary(ItineraryModel model)
        {

            bool result = dal.DeleteItinerary(model);
            return RedirectToAction("_LoginPartial");

        }


    }
}
