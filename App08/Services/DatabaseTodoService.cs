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

        public List<TodoTask> GetAllTasks()
        {
            var taskList = new List<TodoTask>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, TaskDescription, IsCompleted, CreatedAt FROM Tasks " +
                    "ORDER BY CreatedAt DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taskList.Add(new TodoTask
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                TaskDescription = reader["TaskDescription"]?.ToString() ?? "",
                                IsCompleted = Convert.ToBoolean(reader["IsCompleted"]),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            });
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
                string query = "INSERT INTO Tasks (TaskDescription, IsCompleted, CreatedAt)" +
                    " VALUES (@Desc, 0, GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Desc", description);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ToggleTaskStatus(int taskId, bool isCompleted)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Tasks SET IsCompleted = @IsComp WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IsComp", isCompleted ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Id", taskId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(int taskId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Tasks WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", taskId);
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
