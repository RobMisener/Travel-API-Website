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

        public ActionResult Itinerary()
        {
            var itinerary = dal.GetItinerary();

            return PartialView("_LoginPartial", itinerary);
        }

        // GET: Itinerary
        [System.Web.Http.HttpGet]
        public ActionResult DisplayItinerary(int itinId)
        {
            List<ItineraryModel> itineraryModelList = dal.GetItinerary();

            return View(itineraryModelList);
        }


        //POST: Create Itinerary
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/itinerary/")]
        public IHttpActionResult CreateNewItinerary(ItineraryModel model)
        {

            bool result = dal.CreateItinerary(model);

            return RedirectToAction("_LoginPartial");

        }
        //POST: Update Itinerary
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/itinerary/{ItinId")]
        public IHttpActionResult UpdateItinerary(ItineraryModel model)
        {

            bool result = dal.UpdateItinerary(model);
            return Ok();

        }
        //DELETE: Delete Itinerary
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/itinerary/{ItinId}")]
        public IHttpActionResult RemoveItinerary(ItineraryModel model)
        {

            bool result = dal.DeleteItinerary(model);
            return Ok();

        }


    }
}
