using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{

    public class ItineraryDAL: IItineraryDAL
    {

        private string connectionString;

        public ItineraryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private const string SQL_GetItinerary = @"SELECT * FROM Itinerary_Stops JOIN Itinerary on Itinerary_Stops.ItinId = Itinerary.ItinId WHERE ItinId = @ItinId";

        public bool CreateItinerary(int userId, DateTime startDate, List<ItineraryStop> stops)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Itinerary (UserId, StartDate) VALUES (@UserId, @StartDate)", conn);
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

        public bool UpdateItinerary(int userId, DateTime startDate, List<ItineraryStop> stops)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Itinerary (StartDate) SET StartDate = @StartDate", conn);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.ExecuteNonQuery();

                        // delete original stops from itinerary_stops
                        cmd = new SqlCommand("DELETE * from Itinerary_Stops WHERE ItinId = @ItinId", conn);
                        cmd.ExecuteNonQuery();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            foreach (var stop in stops)
                            {
                                SqlCommand newcmd = new SqlCommand($"INSERT INTO Itinerary_Stops (PlaceId, Order, Name, Latitude, Longitude, Category) VALUES (@PlaceId, @Order, @Name, @Latitude, @Longitude, @Category)", conn);
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

        public void DeleteItinerary(int itinID)
        {
            //delete itinerary from table 
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE * FROM Itinerary WHERE ItinId = @itinID", conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading the database: " + ex.Message);
            }
        }

        public List<ItineraryModel> GetItinerary(string ItinId)
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
                            ItineraryModel itineraryModel = new ItineraryModel();
                            itineraryModel.ItinId = Convert.ToInt32(reader["ItinId"]);
                            itineraryModel.UserId = Convert.ToInt32(reader["UserId"]);
                            itineraryModel.StartDate = Convert.ToDateTime(reader["StartDate"]);
                            foreach (var stop in itineraryModel.Stops)
                            {
                                stop.PlaceID = Convert.ToString(reader["PlaceId"]);
                                stop.Name = Convert.ToString(reader["Name"]);
                                stop.Address = Convert.ToString(reader["Address"]);
                                stop.Latitude = Convert.ToDouble(reader["Latitude"]);
                                stop.Longitude = Convert.ToDouble(reader["Longitude"]);
                                stop.Category = Convert.ToString(reader["Category"]);
                            }

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