using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
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

		public void InsertItineraryStops(string location )
		{

		}

		public void InsertItineraryStops(int itinId, string placeId, int order )
        {
            List<ItineraryModel> output = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"INSERT INTO Itinerary_Stops VALUES ('{itinId}', '{placeId}', '{order}'", conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand readercmd = new SqlCommand(@"SELECT * FROM Itinerary_Stops", conn);                                     
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
							ItineraryModel itineraryModel = new itineraryModel
							{
								ItinId = Convert.ToString(reader["ItinId"]),
								PlaceId = Convert.ToString(reader["PlaceId"]),
								Order = Convert.ToString(reader["Order"]),
							};
							
							output.Add(itineraryModel);
						}
					}
					return output;
				}
				catch (SqlException ex)
				{
					Console.WriteLine("An error occurred reading the database: " + ex.Message);
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