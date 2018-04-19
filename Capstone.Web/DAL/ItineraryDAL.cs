using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web
{

    public class ItineraryDAL
    {
        public ItineraryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        string connectionString;

        public int CreateItinerary(string itinName, Guid userId, DateTime startDate, List<ItineraryStop> stops)
        {
            int newItinId = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Itinerary (ItinName, UserId, StartDate) VALUES (@ItinName, @UserId, @StartDate)", conn);

                        //cmd.Parameters.AddWithValue("@ItinId", itinId);
                        cmd.Parameters.AddWithValue("@ItinName", itinName);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.ExecuteNonQuery();

                        // loop through stops, and insert a stop into intenaryStop
                        cmd = new SqlCommand("SELECT * from itinerary WHERE ItinId = (SELECT MAX(ItinId) FROM itinerary);", conn);
                        newItinId = Convert.ToInt32(cmd.ExecuteScalar());


                        foreach (var stop in stops)
                        {
                            cmd = new SqlCommand("INSERT INTO Itinerary_Stops (ItinId, PlaceId, [Order], Name, Address, Latitude, Longitude, Category, Image) VALUES (@ItinId, @PlaceId, @Order, @Name, @Address, @Latitude, @Longitude, @Category, @Image)", conn);
                            cmd.Parameters.AddWithValue("@ItinId", newItinId);
                            cmd.Parameters.AddWithValue("@PlaceId", stop.PlaceID);
                            cmd.Parameters.AddWithValue("@Order", stop.Order);
                            cmd.Parameters.AddWithValue("@Image", stop.Image);
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
                return newItinId;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading the database: " + ex.Message);
                return 0;
            }
        }

        internal void UpdateItinerary()
        {
            throw new NotImplementedException();
        }

        public int UpdateItinerary(int itinId, string itinName, Guid userId, DateTime startDate, List<ItineraryStop> stops)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Itinerary SET StartDate = @StartDate WHERE ItinId = @itinId", conn);
                        cmd.Parameters.AddWithValue("@itinId", itinId);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.ExecuteNonQuery();

                        // delete original stops from itinerary_stops
                        cmd = new SqlCommand("DELETE from Itinerary_Stops WHERE ItinId = @itinId", conn);
                        cmd.Parameters.AddWithValue("@itinId", itinId);

                        cmd.ExecuteNonQuery();

                        foreach (var stop in stops)
                        {
                            cmd = new SqlCommand("INSERT INTO Itinerary_Stops (ItinId, PlaceId, [Order], Name, Address, Latitude, Longitude, Category, Image) VALUES (@ItinId, @PlaceId, @Order, @PhotoRef, @Name, @Address, @Latitude, @Longitude, @Category, @Image)", conn);
                            cmd.Parameters.AddWithValue("@ItinId", itinId);
                            cmd.Parameters.AddWithValue("@PlaceId", stop.PlaceID);
                            cmd.Parameters.AddWithValue("@Order", stop.Order);
                            cmd.Parameters.AddWithValue("@Image", stop.Image);
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
                return itinId;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading the database: " + ex.Message);
                return 0;
            }

        }


		public void DeleteItinerary(int itinId)
        {
            //delete itinerary from table 
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

					SqlCommand cmd = new SqlCommand("DELETE from Itinerary_Stops WHERE ItinId = @itinId", conn);
					cmd.Parameters.AddWithValue("@itinId", itinId);
					cmd.ExecuteNonQuery();

					SqlCommand cmd2 = new SqlCommand($"DELETE FROM Itinerary WHERE ItinId = @itinId", conn);
					cmd2.Parameters.AddWithValue("@itinId", itinId);
					cmd2.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading the database: " + ex.Message);
            }
        }


        private const string SQL_GetItinerary = @"SELECT * FROM Itinerary_Stops JOIN Itinerary on Itinerary_Stops.ItinId = Itinerary.ItinId WHERE UserId = @UserId ORDER BY ItinId, [Order]";

        public List<ItineraryModel> GetItinerary(Guid UserId)
        {

            List<ItineraryModel> output = new List<ItineraryModel>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(@"SELECT * FROM Itinerary WHERE UserId = @UserId ORDER BY StartDate; ", conn);
                        cmd.Parameters.AddWithValue("@UserId", UserId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ItineraryModel itineraryModel = new ItineraryModel();
                            {
                                itineraryModel.ItinId = Convert.ToInt32(reader["ItinId"]);
                                itineraryModel.ItinName = Convert.ToString(reader["ItinName"]);
                                itineraryModel.UserId = Guid.Parse(Convert.ToString(reader["UserId"]));
                                itineraryModel.StartDate = Convert.ToDateTime(reader["StartDate"]);
                                //foreach (var stop in itineraryModel.Stops)
                                //{
                                //    stop.PlaceID = Convert.ToString(reader["PlaceId"]);
                                //    stop.Name = Convert.ToString(reader["Name"]);
                                //    stop.Address = Convert.ToString(reader["Address"]);
                                //    stop.Order = Convert.ToInt32(reader["Order"]);
                                //    stop.Latitude = Convert.ToDouble(reader["Latitude"]);
                                //    stop.Longitude = Convert.ToDouble(reader["Longitude"]);
                                //    stop.Category = Convert.ToString(reader["Category"]);
                                //}
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
        public ItineraryModel GetSingleItinerary(int ItinId)
        {
            ItineraryModel output = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM Itinerary WHERE ItinId = @ItinId; ", conn);
                    cmd.Parameters.AddWithValue("@ItinId", ItinId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        output = new ItineraryModel();

                        output.ItinId = Convert.ToInt32(reader["ItinId"]);
                        output.ItinName = Convert.ToString(reader["ItinName"]);
                        output.UserId = Guid.Parse(Convert.ToString(reader["UserId"]));
                        output.StartDate = Convert.ToDateTime(reader["StartDate"]);

                    }
                    reader.Close();

                    SqlCommand cmd2 = new SqlCommand(@"SELECT * FROM Itinerary_Stops WHERE Itinerary_Stops.ItinId = @ItinId ORDER BY [Order];", conn);
                    cmd2.Parameters.AddWithValue("@ItinId", ItinId);

                    reader = cmd2.ExecuteReader();
                    output.Stops = new List<ItineraryStop>();
                    while (reader.Read())
                    {
                        ItineraryStop stop = new ItineraryStop();
                        stop.PlaceID = Convert.ToString(reader["PlaceId"]);
                        stop.Name = Convert.ToString(reader["Name"]);
                        stop.Image = Convert.ToString(reader["Image"]);
                        stop.Address = Convert.ToString(reader["Address"]);
                        stop.Order = Convert.ToInt32(reader["Order"]);
                        stop.Latitude = Convert.ToDouble(reader["Latitude"]);
                        stop.Longitude = Convert.ToDouble(reader["Longitude"]);
                        stop.Category = Convert.ToString(reader["Category"]);
                        output.Stops.Add(stop);
                    }
                }
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading the database: " + ex.Message);
                return null;
            }

        }
    }




    //private ItineraryModel MapItineraryFromReader(SqlDataReader reader)
    //{
    //    LocationModel Location = new LocationModel
    //    {
    //        ItinId = Convert.ToString(reader["Name"]),
    //        PlaceId = Convert.ToString(reader["PlaceId"]),
    //        Order = Convert.ToString(reader["Order"]),
    //    };

    //    return Location;

    //}

}

