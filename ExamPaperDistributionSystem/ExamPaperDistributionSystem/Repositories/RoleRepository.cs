using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ExamPaperDistributionSystem.Models;

namespace ExamPaperDistributionSystem.Repositories
{
    public class RoleRepository
    {
        private readonly string _connectionString;

        public RoleRepository()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ExamPaperDB"].ConnectionString;
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Roles", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        });
                    }
                }
            }

            return roles;
        }

        public Role GetRoleById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Roles WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Role
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        };
                    }
                }
            }

            return null;
        }

        public Role GetRoleByName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Roles WHERE Name = @name", connection);
                command.Parameters.AddWithValue("@name", name);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Role
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        };
                    }
                }
            }

            return null;
        }

        public void AddRole(Role role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Roles (Name) VALUES (@name)", connection);
                command.Parameters.AddWithValue("@name", role.Name);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateRole(Role role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Roles SET Name = @name WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", role.Id);
                command.Parameters.AddWithValue("@name", role.Name);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteRole(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Roles WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
