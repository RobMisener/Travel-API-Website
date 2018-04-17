//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Capstone.Web.DAL
//{
//	public class RatedPlacesDALcs
//	{
//		public ItineraryDAL(string connectionString)
//		{
//			this.connectionString = connectionString;
//		}

//		private const string SQL_GetItinerary = @"Select *  from Rated_Places";

//		string connectionString;

//		public bool AddPlaceToDatabase(string placeId)
//		{
//			try
//			{
//				using (TransactionScope scope = new TransactionScope())
//				{
//					using (SqlConnection conn = new SqlConnection(connectionString))
//					{
//						conn.Open();
//						SqlCommand cmd = new SqlCommand("INSERT INTO [Rated_Places] (PlaceId) VALUES (@placeId)", conn);
//						cmd.Parameters.AddWithValue("@PlaceId", placeId);
//						cmd.ExecuteNonQuery();

//					}
//					scope.Complete();
//				}
//				return true;
//			}
//			catch (SqlException ex)
//			{
//				Console.WriteLine("An error occurred reading the database: " + ex.Message);
//				return false;
//			}
//		}

//		public List<RatedPlacesModel> GetListRatedPlaces()
//		{
//			List<RatedPlacesModel> results = new List<RatedPlacesModel>();

//			try
//			{
//				using (SqlConnection conn = new SqlConnection(connectionString))
//				{

//					conn.Open();

//					SqlCommand cmd = new SqlCommand(SQL_SelectSurveyCountByParkCode, conn);

//					SqlDataReader reader = cmd.ExecuteReader();

//					while (reader.Read())
//					{
//						RatedPlacesModel result = new RatedPlacesModel
//						{
//							PlaceName = Convert.ToInt32(reader["surveyCount"]),
//							ThumbsUpCount = Convert.ToString(reader["parkCode"]),
//							ThumbsDownCount = Convert.ToString(reader["parkName"])
//						};

//						results.Add(result);
//					}

//					return results;
//				}
//			}
//			catch (SqlException)
//			{
//				throw;
//			}
//		}
//	}
//}