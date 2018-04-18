using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using Capstone.Web.Models;
//using Capstone.Web.DAL;
using System.Configuration;
using Microsoft.AspNet.Identity;
using Ninject;
using Ninject.Web.Common.WebHost;
using Ninject.Web.WebApi;



namespace Capstone.Web.Controllers
{
    public class ItineraryController : ApiController
    {
        ItineraryDAL dal = new ItineraryDAL(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/itinerary/{UserId}")]
        //public IHttpActionResult Get(Guid UserId)
        //{
        //    var username = User.Identity.GetUserName();
        //    var returnedItinerary = dal.GetItinerary(UserId);
        //    return Ok(returnedItinerary);
        //}

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/itinerary/{ItinId}")]
        public IHttpActionResult SingleItinerary(int ItinId)
        {
//          var username = User.Identity.GetUserName();
          var soloItinerary = dal.GetSingleItinerary(ItinId);
          return Ok(soloItinerary);
        }


        //POST: Create Itinerary
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/itinerary")]
        public IHttpActionResult SaveItinerary(ItineraryModel model)
        {
            if (model.ItinId != 0)
            {
                var userId = RequestContext.Principal.Identity.GetUserId();
                model.ItinId = dal.UpdateItinerary(model.ItinId, model.ItinName, Guid.Parse(userId), model.StartDate, model.Stops);

            }
            else
            {
                var userId = RequestContext.Principal.Identity.GetUserId();
                model.ItinId = dal.CreateItinerary(model.ItinName, Guid.Parse(userId), model.StartDate, model.Stops);

            }

            return Ok(model);

        }
        ////POST: Update Itinerary
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/itinerary/{ItinId}")]
        //public IHttpActionResult UpdateItinerary(ItineraryModel model)
        //{
        //    var username = User.Identity.GetUserName();

        //    //dal.UpdateItinerary(model);
        //    return Ok();

        //}

        //DELETE: Delete Itinerary
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/itinerary/{ItinId}")]
        public IHttpActionResult RemoveItinerary(int itinId)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            dal.DeleteItinerary(itinId);
            return Ok();

        }


    }
}
