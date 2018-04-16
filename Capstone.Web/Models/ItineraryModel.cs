using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
	public class ItineraryModel
	{
		public Guid ItinId { get; set; }
		public string ItinName { get; set;}
		public Guid UserId { get; set; }
		public DateTime StartDate { get; set; }
		public List<ItineraryStop> Stops { get; set; }
	}

}