using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ExamPaperDistributionSystem.Models;

namespace ExamPaperDistributionSystem.Repositories
{
    public class LogRepository
    {
        private readonly string _connectionString;

        public LogRepository()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ExamPaperDB"].ConnectionString;
        }

        public void AddLog(Log log)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Logs (UserId, Action, Timestamp) VALUES (@UserId, @Action, @Timestamp)", connection);
                command.Parameters.AddWithValue("@UserId", log.UserId);
                command.Parameters.AddWithValue("@Action", log.Action);
                command.Parameters.AddWithValue("@Timestamp", log.Timestamp);
                command.ExecuteNonQuery();
            }
        }

        public List<Log> GetAllLogs()
        {
            var logs = new List<Log>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Logs", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new Log
                        {
                            UserId = (int)reader["UserId"],
                            Action = (string)reader["Action"],
                            Timestamp = (DateTime)reader["Timestamp"]
                        });
                    }
                }
            }
            return logs;
        }
    }
}
