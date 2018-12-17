using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CombineWebService.Services
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

        //public static List<Models.CombineTest> GetTests()
        //{
        //    List<Models.CombineTest> cList = new List<Models.CombineTest>();

        //    using (var connection = new SqlConnection(connectionString))
        //    using (SqlCommand cmd = new SqlCommand("[dbo].[GetTest]", connection))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        connection.Open();
        //        using (var reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                cList.Add(new Models.CombineTest
        //                {
        //                    CombineTestID = reader.GetInt32(reader.GetOrdinal("TestID")),
        //                    Test = reader.GetString(reader.GetOrdinal("Test")),
        //                    Measurement = reader.GetString(reader.GetOrdinal("Measurement")),
        //                });
        //            }
        //        }
        //        connection.Close();
        //    }
        //    return cList;
        //}

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

        public static List<Models.Participants> GetCombineParticipants(int combineID)
        {
            List<Models.Participants> cList = new List<Models.Participants>();

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetCombineParticipants]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineID", combineID);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cList.Add(new Models.Participants
                        {
                            ParticipantID = reader.GetInt32(reader.GetOrdinal("ParticipantID")),
                            ParticipantFirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            ParticipantsLastName = reader.GetString(reader.GetOrdinal("LastName")),
                            CombineName = reader.GetString(reader.GetOrdinal("CombineName")),
                        });
                    }
                }
                connection.Close();
            }
            return cList;
        }

        public static List<Models.CombineTest> GetParticipantTest(int combineID, int participantID)
        {
            List<Models.CombineTest> cList = new List<Models.CombineTest>();

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetParticipantTest]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineID", combineID);
                cmd.Parameters.AddWithValue("@ParticipantID", participantID);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cList.Add(new Models.CombineTest
                        {
                            CombineTestID = reader.GetInt32(reader.GetOrdinal("ID")),
                            Test = reader.GetString(reader.GetOrdinal("Test")),
                            Participant = reader.GetString(reader.GetOrdinal("Participant")),
                            //Result = reader.IsDBNull(reader.GetOrdinal("Result")) ? null : reader.GetString(reader.GetOrdinal("Result")),
                            //Measurement = reader.GetString(reader.GetOrdinal("Measurement")),
                        });
                    }
                }
                connection.Close();
            }
            return cList;
        }

        public static List<Models.CombineResult> GetTestResults(int participantTestId)
        {
            List<Models.CombineResult> cList = new List<Models.CombineResult>();

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetParticipantTestResults]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ParticipantTestID", participantTestId);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cList.Add(new Models.CombineResult
                        {
                            CombineTestID = reader.GetInt32(reader.GetOrdinal("CombineID")),
                            ParticipantTestID = reader.GetInt32(reader.GetOrdinal("ID")),
                            ParticipantID = reader.GetInt32(reader.GetOrdinal("ParticipantID")),
                            Result = reader.IsDBNull(reader.GetOrdinal("Result")) ? null : reader.GetString(reader.GetOrdinal("Result")),
                            Measurement = reader.GetString(reader.GetOrdinal("Measurement")),
                            Attempt = reader.GetInt32(reader.GetOrdinal("Attempt")),
                            IncludeRepCount = reader.GetBoolean(reader.GetOrdinal("IncludeRepCount")),
                            NumberOfReps = reader.IsDBNull(reader.GetOrdinal("NumberOfReps")) ? null : reader.GetString(reader.GetOrdinal("NumberOfReps")),
                            PageTitle = reader.GetString(reader.GetOrdinal("PageTitle")),
                        });
                    }
                }
                connection.Close();
            }
            return cList;
        }

        public static void UpdateParticipantTest(int id, string result, int numberOfReps)
        {
            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[UpdateParticipantResults]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Result", result);
                cmd.Parameters.AddWithValue("@NumberOfReps", numberOfReps);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}