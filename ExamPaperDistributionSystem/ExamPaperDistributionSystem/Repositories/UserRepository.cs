using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ExamPaperDistributionSystem.Models;

namespace ExamPaperDistributionSystem.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ExamPaperDB"].ConnectionString;
        }

        public User GetUserById(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Users WHERE Id = @userId", connection);
                command.Parameters.AddWithValue("@userId", userId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Role = new Role
                            {
                                Id = (int)reader["RoleId"],
                                Name = (string)reader["RoleName"]
                            }
                        };
                    }
                }
            }
            return null;
        }

        public User GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Users WHERE Username = @username", connection);
                command.Parameters.AddWithValue("@username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Role = new Role
                            {
                                Id = (int)reader["RoleId"],
                                Name = (string)reader["RoleName"]
                            }
                        };
                    }
                }
            }
            return null;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Users", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"],
                            Role = new Role
                            {
                                Id = (int)reader["RoleId"],
                                Name = (string)reader["RoleName"]
                            }
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Users (Username, Password, RoleId, RoleName) VALUES (@username, @password, @roleId, @roleName)", connection);
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@roleId", user.Role.Id);
                command.Parameters.AddWithValue("@roleName", user.Role.Name);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Users SET Username = @username, Password = @password, RoleId = @roleId, RoleName = @roleName WHERE Id = @userId", connection);
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@roleId", user.Role.Id);
                command.Parameters.AddWithValue("@roleName", user.Role.Name);
                command.Parameters.AddWithValue("@userId", user.Id);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Users WHERE Id = @userId", connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.ExecuteNonQuery();
            }
        }
    }
}
