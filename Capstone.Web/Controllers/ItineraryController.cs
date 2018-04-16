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
using System.Configuration;
using Microsoft.AspNet.Identity;

namespace Capstone.Web.Controllers
{
    public class ItineraryController : ApiController
    {
        ItineraryDAL dal = new ItineraryDAL(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/itinerary")]
        public IHttpActionResult Get()
        {

            return Ok();
        }


        //POST: Create Itinerary
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/itinerary")]
        public IHttpActionResult SaveItinerary(ItineraryModel model)
        {
            if (model.ItinId != 0)
            {
                //dal.UpdateItinerary(model);
            }
            else
            {               
                var userId = RequestContext.Principal.Identity.GetUserId();                
                dal.CreateItinerary(model.ItinName, Guid.Parse(userId), model.StartDate, model.Stops);
            }

            return Ok();

        }
        //POST: Update Itinerary
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/itinerary/{ItinId")]
        //public IHttpActionResult UpdateItinerary(ItineraryModel model)
        //{

        //    bool result = dal.UpdateItinerary(model);
        //    return Ok();

        //}
        //DELETE: Delete Itinerary
        //[System.Web.Http.HttpDelete]
        //[System.Web.Http.Route("api/itinerary/{ItinId}")]
        //public IHttpActionResult RemoveItinerary(ItineraryModel model)
        //{

        //    bool result = dal.DeleteItinerary(model);
        //    return Ok();

        //}


    }
}
