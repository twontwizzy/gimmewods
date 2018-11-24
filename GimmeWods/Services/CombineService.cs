using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GimmeWods.Services
{
    public class CombineService
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static void InsertParticipant(string firstName, string lastName)
        {
            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[InsertParticipants]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<Models.Participants> GetParticipants()
        {
            List<Models.Participants> pList = new List<Models.Participants>();

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetParticipants]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pList.Add(new Models.Participants
                        {
                            ParticipantID = reader.GetInt32(reader.GetOrdinal("ParticipantId")),
                            ParticipantFirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            ParticipantsLastName = reader.GetString(reader.GetOrdinal("LastName")),
                        });
                    }
                }
                connection.Close();
            }
            return pList;
        }

        public static void InsertTests(string test, string measurement)
        {
            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[InsertTest]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Test", test);
                cmd.Parameters.AddWithValue("@Measurement",measurement);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<Models.CombineTest> GetTests()
        {
            List<Models.CombineTest> cList = new List<Models.CombineTest>();

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetTest]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cList.Add(new Models.CombineTest
                        {
                            CombineTestID = reader.GetInt32(reader.GetOrdinal("TestID")),
                            Test = reader.GetString(reader.GetOrdinal("Test")),
                            Measurement = reader.GetString(reader.GetOrdinal("Measurement")),
                        });
                    }
                }
                connection.Close();
            }
            return cList;
        }

        public static int InsertCombine(string combineName, DateTime combineDate)
        {
            int id = 0;

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[InsertCombine]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineName", combineName);
                cmd.Parameters.AddWithValue("@CombineDate", combineDate);
                connection.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }

            return id;
        }

        public static void InsertCombineTests(int combineID, int testID)
        {
            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[InsertCombineTests]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineID", combineID);
                cmd.Parameters.AddWithValue("@testId", testID);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void InsertCombineParticipants(int combineID, int participantID, int testID)
        {
            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[InsertParticipantTest]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineID", combineID);
                cmd.Parameters.AddWithValue("@TestId", testID);
                cmd.Parameters.AddWithValue("@ParticipantID", participantID);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<Models.Combine> GetCombines()
        {
            List<Models.Combine> cList = new List<Models.Combine>();

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetCombines]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cList.Add(new Models.Combine
                        {
                            CombineID = reader.GetInt32(reader.GetOrdinal("CombineID")),
                            CombineName = reader.GetString(reader.GetOrdinal("CombineName")),
                            CombineDate = reader.GetDateTime(reader.GetOrdinal("CombineDate")),
                        });
                    }
                }
                connection.Close();
            }
            return cList;
        }
    }
}