using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Model class to store the places (stores thumbs up/down info for them)
/// </summary>

namespace Capstone.Web.Models
{
	public class RatedPlacesModel
	{
		public string PlaceId { get; set; }
		public string PlaceName { get; set; }
		public int ThumbsUpCount { get; set; }
		public int ThumbsDownCount { get; set; }
	}
}