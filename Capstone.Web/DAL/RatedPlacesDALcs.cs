using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
	public class RatedPlacesDALcs
	{

		public bool AddPlaceToDatabase(string name, int placeId)
		{
				try
				{
					using (TransactionScope scope = new TransactionScope())
					{
						using (SqlConnection conn = new SqlConnection(connectionString))
						{
							conn.Open();
							SqlCommand cmd = new SqlCommand("INSERT INTO Itinerary (ItinName, UserId, StartDate) VALUES (@ItinName, @UserId, @StartDate)", conn);
							cmd.Parameters.AddWithValue("@ItinName", itinName);
							cmd.Parameters.AddWithValue("@UserId", userId);
							cmd.Parameters.AddWithValue("@StartDate", startDate);
							cmd.ExecuteNonQuery();

							// loop through stops, and insert a stop into intenaryStop
							cmd = new SqlCommand("SELECT * from itinerary WHERE ItinId = (SELECT MAX(ItinId) FROM itinerary);", conn);

							SqlDataReader reader = cmd.ExecuteReader();

							if (reader.Read())
							{
								var newItinId = Convert.ToInt32(reader["ItinId"]);
								foreach (var stop in stops)
								{
									SqlCommand newcmd = new SqlCommand("INSERT INTO Itinerary_Stops (PlaceId, Order, Name, Latitude, Longitude, Category) VALUES (@PlaceId, @Order, @Name, @Latitude, @Longitude, @Category)", conn);
									cmd.Parameters.AddWithValue("@PlaceId", stop.PlaceID);
									cmd.Parameters.AddWithValue("@Order", stop.Order);
									cmd.Parameters.AddWithValue("@Name", stop.Name);
									cmd.Parameters.AddWithValue("@Latitude", stop.Latitude);
									cmd.Parameters.AddWithValue("@Longitude", stop.Longitude);
									cmd.Parameters.AddWithValue("@Category", stop.Category);
									cmd.ExecuteNonQuery();

								}
							}
						}
						scope.Complete();
					}
					return true;
				}
				catch (SqlException ex)
				{
					Console.WriteLine("An error occurred reading the database: " + ex.Message);
					return false;
				}
			}

		}
}