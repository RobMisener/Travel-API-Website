using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Capstone.Web.Models;


namespace Capstone.Web.DAL
{
	public interface IItineraryDAL
	{
        bool CreateItinerary(ItineraryModel model);
        bool UpdateItinerary(ItineraryModel model);
        bool DeleteItinerary(ItineraryModel model);
        List<ItineraryModel> GetItinerary();


	}
}