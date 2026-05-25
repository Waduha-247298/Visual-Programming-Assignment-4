using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace App08.Services
{
    public class DatabaseTodoService
    {
        private readonly string _connectionString = 
            @"Server=LAPTOP-DTEKK0BC\SQLEXPRESS;Database=ToDoApp;Trusted_Connection=True;
             TrustServerCertificate=True;";
       
        public List<KeyValuePair<int, string>> GetAllTasks()
        {
            var taskList = new List<KeyValuePair<int, string>>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, TaskDescription FROM Tasks";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string idRaw = reader["Id"]?.ToString() ?? "0";
                            string desc = reader["TaskDescription"]?.ToString() ?? "";

                            int id = int.Parse(idRaw);
                            taskList.Add(new KeyValuePair<int, string>(id, desc));
                        }
                    }
                }
            }
            return taskList;
        }
        public void AddTask(string description)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Tasks (TaskDescription, IsCompleted) VALUES (@Desc, 0)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Desc", description);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteTask(int taskId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Tasks WHERE Id = " + taskId;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ClearAllTasks()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "TRUNCATE TABLE Tasks";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
