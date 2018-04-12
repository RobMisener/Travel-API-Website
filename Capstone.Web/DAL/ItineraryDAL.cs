using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web
{

	public class ItineraryDAL
	{

		private const string SQL_GetItinerary = @"SELECT * FROM Itinerary_Stops WHERE ItinId = @ItinId";

		string connectionString;
		//const string SQL_SelectParkByParkCode = @"SELECT * FROM database WHERE parkCode = @parkCode;";

		public ItineraryDAL(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public void InsertItineraryStops(int itinId, string placeId, int order)
		{
			List<ItineraryModel> output = new List<ItineraryModel>();
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand($"INSERT INTO Itinerary_Stops VALUES ('{itinId}', '{placeId}', '{order}'", conn);
					cmd.ExecuteNonQuery();
					//SqlCommand readercmd = new SqlCommand(@"SELECT * FROM Itinerary_Stops", conn);
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("An error occurred reading the database: " + ex.Message);
			}
		}

		public void DeleteItineraryStops(int itinID, string placeId)
		{
			//delete from table where order = order
			List<ItineraryModel> output = new List<ItineraryModel>();
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand($"DELETE FROM Itinerary_Stops WHERE ItinId = @itinID AND PlaceId = @placeId", conn);
					cmd.ExecuteNonQuery();
					//SqlCommand readercmd = new SqlCommand(@"SELECT * FROM Itinerary_Stops", conn);
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine("An error occurred reading the database: " + ex.Message);
			}
		}

		public List<ItineraryModel> GetItineraryStopsById(string ItinId)
		{
			List<ItineraryModel> output = new List<ItineraryModel>();
			{
				try
				{
					using (SqlConnection conn = new SqlConnection(connectionString))
					{
						conn.Open();
						SqlCommand cmd = new SqlCommand(SQL_GetItinerary, conn);
						cmd.Parameters.AddWithValue("@ItinId", ItinId);
						SqlDataReader reader = cmd.ExecuteReader();

						while (reader.Read())
						{
							ItineraryModel itineraryModel = new ItineraryModel
							{
								ItinId = Convert.ToInt32(reader["ItinId"]),
								PlaceId = Convert.ToString(reader["PlaceId"]),
								Order = Convert.ToInt32(reader["Order"]),
							};

							output.Add(itineraryModel);
						}
						return output;
					}
				}
				catch (SqlException ex)
				{
					Console.WriteLine("An error occurred reading the database: " + ex.Message);
					return output;
				}

			}
		}


		//private ItineraryModel MapItineraryFromReader(SqlDataReader reader)
		//{
		//	LocationModel Location = new LocationModel
		//	{
		//		ItinId = Convert.ToString(reader["Name"]),
		//		PlaceId = Convert.ToString(reader["PlaceId"]),
		//		Order = Convert.ToString(reader["Order"]),
		//	};

		//	return Location;

		//}

	}
}