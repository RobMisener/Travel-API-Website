using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{

	public class ItineraryStop
	{
        public string PlaceID { get; set; }
        public int Order { get; set; }
		public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
	}
}
