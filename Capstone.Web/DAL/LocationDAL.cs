//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Capstone.Web.Models;
//using System.Data.SqlClient;

//namespace Capstone.Web.DAL
//{
//	public class LocationDAL
//	{

//		string connectionString;

//		//const string SQL_SelectParkByParkCode = @"SELECT * FROM database WHERE parkCode = @parkCode;";

//		public LocationDAL(string connectionString)
//		{
//			this.connectionString = connectionString;
//		}

//		public List<LocationModel> GetAllLocations()
//		{
//			List<LocationModel> output = new List<LocationModel>();

//			try
//			{
//				using (SqlConnection conn = new SqlConnection(connectionString))
//				{
//					conn.Open();
//					SqlCommand cmd = new SqlCommand(@"Select * from database", conn);

//					SqlDataReader reader = cmd.ExecuteReader();

//					while (reader.Read())
//					{
//						LocationModel location = MapLocationFromReader(reader);
//						output.Add(location);
//					}

//					return output;
//				}
//			}
//			catch (SqlException)
//			{
//				throw;
//			}

//		}

//		private LocationModel MapLocationFromReader(SqlDataReader reader)
//		{
//			LocationModel Location = new LocationModel
//			{
//				Name = Convert.ToString(reader["Name"]),
//				PlaceId = Convert.ToString(reader["PlaceId"]),
//				City = Convert.ToString(reader["City"]),
//				Latitude = Convert.ToDouble(reader["Latitude"]),
//				Longitude = Convert.ToDouble(reader["Category"])
//			};

//			return Location;

//		}
//	}
//}