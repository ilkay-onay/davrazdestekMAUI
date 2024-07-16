using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<List<Call>> GetCallsAsync(int pageNumber, int pageSize)
        {
            var calls = new List<Call>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"
            SELECT [Id], [Tarih], [Arayan], [Aranan], [ToplamSure], [BeklemeSuresi], [GorusmeSuresi], [Sonuc], [Tipi]
            FROM [Dv_Destek_Cagrilar]
            ORDER BY [Tarih] DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                    command.Parameters.AddWithValue("@PageSize", pageSize);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            calls.Add(new Call
                            {
                                Id = reader.GetInt32(0),
                                Tarih = reader.GetDateTime(1),
                                Arayan = reader.GetString(2),
                                Aranan = reader.GetString(3),
                                ToplamSure = reader.GetTimeSpan(4),
                                BeklemeSuresi = reader.GetTimeSpan(5),
                                GorusmeSuresi = reader.GetTimeSpan(6),
                                Sonuc = reader.GetInt32(7),
                                Tipi = reader.GetInt16(8)
                            });
                        }
                    }
                }
            }
            return calls;
        }

    }

    public class Call
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public string Arayan { get; set; }
        public string Aranan { get; set; }
        public TimeSpan ToplamSure { get; set; }
        public TimeSpan BeklemeSuresi { get; set; }
        public TimeSpan GorusmeSuresi { get; set; }
        public int Sonuc { get; set; }
        public short Tipi { get; set; }
    }
}

