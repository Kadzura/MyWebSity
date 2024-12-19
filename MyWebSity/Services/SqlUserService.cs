using Microsoft.Data.SqlClient;
using MyWebSity.Models;

namespace MyWebSity.Services
{
    public class SqlUserService : ISqlUserService
    {
        private readonly string _connectionString;

        public SqlUserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationUser CreateUser(string email, string password)
        {
            ApplicationUser user = new ApplicationUser() { Email = email, Password = password };
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Users (Email, Password) VALUES (@Email, @Password); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    user.Id = id;

                }
            }
            return user;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            ApplicationUser user = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT Id, Email, Password FROM Users WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new ApplicationUser
                            {
                                Id = (int)reader["Id"],
                                Email = (string)reader["Email"],
                                Password = (string)reader["Password"]
                            };
                        }
                    }
                }
            }
            return user;
        }
        public bool ValidateUser(string email, string password)
        {
            var user = GetUserByEmail(email);
            return user != null && user.Password == password;
        }
    }
}
