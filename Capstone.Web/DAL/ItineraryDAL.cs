using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using Capstone.Web.Models;
using System.Data.SqlClient;
using Capstone.Web.DAL;

namespace Capstone.Web
{

    public class ItineraryDAL
    {
        public ItineraryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private const string SQL_GetItinerary = @"SELECT * FROM Itinerary_Stops JOIN Itinerary on Itinerary_Stops.ItinId = Itinerary.ItinId WHERE ItinId = @ItinId";

        string connectionString;

        public bool CreateItinerary(string itinName, Guid userId, DateTime startDate, List<ItineraryStop> stops)
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
                        int newItinId = Convert.ToInt32(cmd.ExecuteScalar());


                        foreach (var stop in stops)
                        {
                            cmd = new SqlCommand("INSERT INTO Itinerary_Stops (ItinId, PlaceId, [Order], Name, Address, Latitude, Longitude, Category) VALUES (@ItinId, @PlaceId, @Order, @Name, @Address, @Latitude, @Longitude, @Category)", conn);
                            cmd.Parameters.AddWithValue("@ItinId", newItinId);
                            cmd.Parameters.AddWithValue("@PlaceId", stop.PlaceID);
                            cmd.Parameters.AddWithValue("@Order", stop.Order);
                            cmd.Parameters.AddWithValue("@Name", stop.Name);
                            cmd.Parameters.AddWithValue("@Address", stop.Address);
                            cmd.Parameters.AddWithValue("@Latitude", stop.Latitude);
                            cmd.Parameters.AddWithValue("@Longitude", stop.Longitude);
                            cmd.Parameters.AddWithValue("@Category", stop.Category);
                            cmd.ExecuteNonQuery();

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

        public bool UpdateItinerary(Guid itinId, Guid userId, DateTime startDate, List<ItineraryStop> stops)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Itinerary (StartDate) WHERE ItinId = @itinId SET StartDate = @StartDate", conn);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.ExecuteNonQuery();

                        // delete original stops from itinerary_stops
                        cmd = new SqlCommand("DELETE * from Itinerary_Stops WHERE ItinId = @itinId", conn);
                        cmd.ExecuteNonQuery();

                        foreach (var stop in stops)
                        {
                            cmd = new SqlCommand($"INSERT INTO Itinerary_Stops (ItinId, PlaceId, [Order], Name, Latitude, Longitude, Category) VALUES (@PlaceId, @Order, @Name, @Latitude, @Longitude, @Category)", conn);
                            cmd.Parameters.AddWithValue("@ItinId", ItinId);
                            cmd.Parameters.AddWithValue("@PlaceId", stop.PlaceID);
                            cmd.Parameters.AddWithValue("@Order", stop.Order);
                            cmd.Parameters.AddWithValue("@Name", stop.Name);
                            cmd.Parameters.AddWithValue("@Address", stop.Address);
                            cmd.Parameters.AddWithValue("@Latitude", stop.Latitude);
                            cmd.Parameters.AddWithValue("@Longitude", stop.Longitude);
                            cmd.Parameters.AddWithValue("@Category", stop.Category);
                            cmd.ExecuteNonQuery();

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
        public void DeleteItinerary(Guid itinID)
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

        public List<ItineraryModel> GetItinerary(Guid ItinId)
        {
            List<ItineraryModel> output = new List<ItineraryModel>();
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            SqlCommand cmd = new SqlCommand(SQL_GetItinerary, conn);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                ItineraryModel itineraryModel = new ItineraryModel();
                                {
                                    itineraryModel.ItinId = Convert.ToString(reader["ItinId"]);
                                    itineraryModel.ItinName = Convert.ToString(reader["ItinName"]);
                                    itineraryModel.UserId = Convert.ToString(reader["UserId"]);
                                    itineraryModel.StartDate = Convert.ToDateTime(reader["StartDate"]);
                                    foreach (var stop in itineraryModel.Stops)
                                    {
                                        stop.PlaceID = Convert.ToString(reader["PlaceId"]);
                                        stop.Name = Convert.ToString(reader["Name"]);
                                        stop.Address = Convert.ToString(reader["Address"]);
                                        stop.Order = Convert.ToInt32(reader["Order"]);
                                        stop.Latitude = Convert.ToDouble(reader["Latitude"]);
                                        stop.Longitude = Convert.ToDouble(reader["Longitude"]);
                                        stop.Category = Convert.ToString(reader["Category"]);
                                    }
                                    output.Add(itineraryModel);
                                }
                            }
                            scope.Complete();
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


        private ItineraryModel MapItineraryFromReader(SqlDataReader reader)
        {
            LocationModel Location = new LocationModel
            {
                ItinId = Convert.ToString(reader["Name"]),
                PlaceId = Convert.ToString(reader["PlaceId"]),
                Order = Convert.ToString(reader["Order"]),
            };

            return Location;

        }

    }
}