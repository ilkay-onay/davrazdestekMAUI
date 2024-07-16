using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MauiApp1.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT COUNT(*) FROM Dv_Destek_Personel WHERE [E-posta] = @Email AND [Sifre] = @Password";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        var result = (int)await command.ExecuteScalarAsync();
                        return result > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log SQL specific exceptions
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log general exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
    }
}
