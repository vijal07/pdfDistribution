using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ExamPaperDistributionSystem.Models;

namespace ExamPaperDistributionSystem.Repositories
{
    public class ExamPaperRepository
    {
        private readonly string _connectionString;

        public ExamPaperRepository()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ExamPaperDB"].ConnectionString;
        }

        public List<ExamPaper> GetAllExamPapers()
        {
            List<ExamPaper> examPapers = new List<ExamPaper>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM ExamPapers", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        examPapers.Add(new ExamPaper
                        {
                            PaperId = (int)reader["Id"],
                            Title = (string)reader["Title"],
                            FileName = (string)reader["FileName"],
                            UploadedAt = (DateTime)reader["UploadDate"],
                            UploadedBy = (string)reader["UploadedBy"]
                            // Add more properties as per your database schema
                        });
                    }
                }
            }

            return examPapers;
        }

        public void UploadExamPaper(ExamPaper examPaper)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO ExamPapers (Title, FileName, UploadDate, UploadedBy) VALUES (@title, @fileName, @uploadDate, @uploadedBy)", connection);
                command.Parameters.AddWithValue("@title", examPaper.Title);
                command.Parameters.AddWithValue("@fileName", examPaper.FileName);
                command.Parameters.AddWithValue("@uploadDate", examPaper.UploadedAt);
                command.Parameters.AddWithValue("@uploadedBy", examPaper.UploadedBy);
                // Add more parameters as per your database schema

                command.ExecuteNonQuery();
            }
        }

        // Add more methods for update, delete, specific queries, etc., as needed
    }
}
