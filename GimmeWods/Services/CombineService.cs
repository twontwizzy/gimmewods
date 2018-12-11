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

        public static List<Models.Participants> GetParticipants(int combineID)
        {
            List<Models.Participants> pList = new List<Models.Participants>();

            var selectedParticipants = GetCombineParticipants(combineID);

            var participants = GetParticipants();

            foreach (var item in participants)
            {
                Models.Participants p = new Models.Participants();

                p.ParticipantID = item.ParticipantID;
                p.ParticipantFirstName = item.ParticipantFirstName;
                p.ParticipantsLastName = item.ParticipantsLastName;

                foreach (var s in selectedParticipants)
                {
                    if (s.ParticipantID == item.ParticipantID)
                    {
                        p.Checked = true;
                    }
                    
                }
                pList.Add(p);
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

        public static List<Models.CombineTest> GetCombineTests(int combineID)
        {
            List<Models.CombineTest> cList = new List<Models.CombineTest>();

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetCombineTests]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineID", combineID);
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
                            Attempts = reader.GetInt32(reader.GetOrdinal("Attempts")),
                            IncludeInCombine = reader.GetBoolean(reader.GetOrdinal("IncludeInCombine")),
                            InclueRepCount = reader.GetBoolean(reader.GetOrdinal("IncludeRepCount")),
                        });
                    }
                }
                connection.Close();
            }
            return cList;
        }

        public static List<Models.CombineTest> GetTests(int combineID)
        {
            List<Models.CombineTest> cList = new List<Models.CombineTest>();

            var selectedTests = GetCombineTests(combineID);

            var test = GetTests();

            foreach (var item in test)
            {
                Models.CombineTest c = new Models.CombineTest();

                c.CombineTestID = item.CombineTestID;
                c.Test = item.Test;
                c.Measurement = item.Measurement;
                

                foreach (var s in selectedTests)
                {
                    if (s.CombineTestID == item.CombineTestID)
                    {
                        c.IncludeInCombine = s.IncludeInCombine;
                        c.Attempts = s.Attempts;
                        c.IncludeInCombine = s.IncludeInCombine;
                        c.InclueRepCount = s.InclueRepCount;
                    }
                    
                }
                cList.Add(c);
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

        public static void UpdateCombine(string combineName, DateTime combineDate, int combineID)
        {
            int id = 0;

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[UpdateCombine]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineName", combineName);
                cmd.Parameters.AddWithValue("@CombineDate", combineDate);
                cmd.Parameters.AddWithValue("@CombineID", combineID);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void InsertCombineTests(int combineID, int testID, bool includeInCombine, int attempts, bool includeRepCount)
        {
            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[InsertCombineTests]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineID", combineID);
                cmd.Parameters.AddWithValue("@testId", testID);
                cmd.Parameters.AddWithValue("@IncludeInCombine", includeInCombine);
                cmd.Parameters.AddWithValue("@Attempts", attempts);
                cmd.Parameters.AddWithValue("@IncludeRepCount", includeRepCount);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void DeleteCombineTests(int combineID)
        {
            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[DeleteCombineTests]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineID", combineID);
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

        public static void DeleteParticipantTests(int combineID)
        {
            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[DeleteParticipantTests]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CombineID", combineID);
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
                        });
                    }
                }
                connection.Close();
            }
            return cList;
        }
    }
}