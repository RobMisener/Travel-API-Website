using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
	public class RatedPlacesDALcs
	{

		public bool AddPlaceToDatabase(string placeId)
		{
				try
				{
					using (TransactionScope scope = new TransactionScope())
					{
						using (SqlConnection conn = new SqlConnection(connectionString))
						{
							conn.Open();
							SqlCommand cmd = new SqlCommand("INSERT INTO Itinerary (placeId) VALUES (@PlaceId)", conn);
							cmd.Parameters.AddWithValue("@PlaceId", placeId);

							cmd.ExecuteNonQuery();

							//cmd = new SqlCommand(@"Select * from Rated_Places", conn);
							//SqlDataReader reader = cmd.ExecuteReader();

							//if (reader.Read())
							//{

							//		SqlCommand newcmd = new SqlCommand("INSERT INTO Itinerary (placeId) VALUES (@PlaceId), conn);
							//		cmd.Parameters.AddWithValue("@PlaceId", placeId);

							//		cmd.ExecuteNonQuery();
							//}
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