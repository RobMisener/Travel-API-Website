using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
	public class ItineraryModel
	{
		public int ItinId { get; set; }
		public string ItinName { get; set;}
		public int UserId { get; set; }
		public DateTime StartDate { get; set; }
		public List<ItineraryStop> Stops { get; set; }
	}

}