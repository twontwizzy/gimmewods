using GimmeWods.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GimmeWods.Services
{
    public class WODService
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static RandomWod GetRandomWod(bool isOutside, string type, string movement, string equipment)
        {
            RandomWod model = new RandomWod();

            using (var connection = new SqlConnection(connectionString))           
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetRandomWod]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@isOutside", isOutside);
                if (type == "0")
                {
                    cmd.Parameters.AddWithValue("@wodtype", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@wodtype", type);
                }
                if (string.IsNullOrEmpty(movement))
                {
                    cmd.Parameters.AddWithValue("@movement", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@movement", movement);
                }
                if (string.IsNullOrEmpty(equipment))
                {
                    cmd.Parameters.AddWithValue("@equipment", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@equipment", equipment);
                }
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        model.WODName = reader.GetString(reader.GetOrdinal("WOD"));
                        model.WODID = reader.GetInt32(reader.GetOrdinal("WODID"));
                        model.SelectedEquipment = equipment.Split(',').ToList();
                        model.SelectedMovement = movement.Split(',').ToList();
                        model.IsOutsite = isOutside;
                        model.WodType = type;
                    }
                }
                connection.Close();
            }
            return model;
        }

        public static List<Models.WodExercise> GetExercises(int wodID)
        {
            var exercises = new List<Models.WodExercise>();

            using (var connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[GetWodExercises]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@wodID", wodID);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exercises.Add(new Models.WodExercise
                        {
                            Exercise = reader.GetString(reader.GetOrdinal("Exercise")),
                            ExerciseID = reader.GetInt32(reader.GetOrdinal("ExerciseID"))
                        });
                    }
                }
                connection.Close();
            }
            return exercises;
        }
    }
}