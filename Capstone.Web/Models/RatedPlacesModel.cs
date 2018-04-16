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
		public string PlaceId;
		public string PlaceName;
		public int ThumbsUpCount;
		public int ThumbsDownCount;
	}
}