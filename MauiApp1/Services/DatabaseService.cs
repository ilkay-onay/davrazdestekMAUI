using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<RemoteConnection>> GetAllRemoteConnectionsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Dv_Destek_Uzak";
                return await connection.QueryAsync<RemoteConnection>(query);
            }
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT COUNT(*) FROM Dv_Destek_Personel WHERE [E-posta] = @Email AND [Sifre] = @Password";
                var result = await connection.ExecuteScalarAsync<int>(query, new { Email = email, Password = password });
                return result > 0;
            }
        }

        public async Task<IEnumerable<CallRecord>> GetCallsAsync(int pageNumber, int pageSize)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                    SELECT * FROM (
                        SELECT 
                            ROW_NUMBER() OVER (ORDER BY Tarih DESC) AS RowNum,
                            Id, Tarih, Arayan, Aranan, ToplamSure, BeklemeSuresi, GorusmeSuresi, Sonuc, Tipi
                        FROM dbo.Dv_Destek_Cagrilar
                    ) AS RowConstrainedResult
                    WHERE RowNum >= @RowStart
                        AND RowNum < @RowEnd
                    ORDER BY RowNum";

                    int rowStart = (pageNumber - 1) * pageSize + 1;
                    int rowEnd = pageNumber * pageSize + 1;

                    return await connection.QueryAsync<CallRecord>(query, new { RowStart = rowStart, RowEnd = rowEnd });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCallsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<int> GetTotalCallCountAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT COUNT(*) FROM dbo.Dv_Destek_Cagrilar";
                    return await connection.ExecuteScalarAsync<int>(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTotalCallCountAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<RemoteConnection>> GetRemoteConnectionsAsync(int pageNumber, int pageSize)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                    SELECT * FROM (
                        SELECT 
                            ROW_NUMBER() OVER (ORDER BY BaglantiTarih DESC) AS RowNum,
                            ID, Baglanan, BaglananUniq, BaglananIp, Yon, Musteri, MusteriUniq, MusteriIp, BaglantiSaat, BaglantiTarih, BaglantiSure, BaglantiAciklama, BaglantiDestekNo, BaglantiUniq, MusteriErpId, DestekUrunId, TalepDetay, ToplamSure
                        FROM dbo.Dv_Destek_Uzak
                    ) AS RowConstrainedResult
                    WHERE RowNum >= @RowStart
                        AND RowNum < @RowEnd
                    ORDER BY RowNum";

                    int rowStart = (pageNumber - 1) * pageSize + 1;
                    int rowEnd = pageNumber * pageSize + 1;

                    return await connection.QueryAsync<RemoteConnection>(query, new { RowStart = rowStart, RowEnd = rowEnd });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetRemoteConnectionsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<int> GetTotalRemoteConnectionCountAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT COUNT(*) FROM dbo.Dv_Destek_Uzak";
                    return await connection.ExecuteScalarAsync<int>(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTotalRemoteConnectionCountAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<int> ExecuteQueryAsync(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(query, parameters);
            }
        }


        public async Task<int> UpdateRemoteConnectionAsync(RemoteConnection remoteConnection)
        {
            var query = @"
                UPDATE Dv_Destek_Uzak 
                SET 
                    Baglanan = @Baglanan,
                    BaglananUniq = @BaglananUniq,
                    BaglananIp = @BaglananIp,
                    Yon = @Yon,
                    Musteri = @Musteri,
                    MusteriUniq = @MusteriUniq,
                    MusteriIp = @MusteriIp,
                    BaglantiSaat = @BaglantiSaat,
                    BaglantiTarih = @BaglantiTarih,
                    BaglantiSure = @BaglantiSure,
                    BaglantiAciklama = @BaglantiAciklama,
                    BaglantiDestekNo = @BaglantiDestekNo,
                    BaglantiUniq = @BaglantiUniq,
                    MusteriErpId = @MusteriErpId,
                    DestekUrunId = @DestekUrunId,
                    TalepDetay = @TalepDetay,
                    ToplamSure = @ToplamSure
                WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = await connection.ExecuteAsync(query, new
                {
                    remoteConnection.Baglanan,
                    remoteConnection.BaglananUniq,
                    remoteConnection.BaglananIp,
                    remoteConnection.Yon,
                    remoteConnection.Musteri,
                    remoteConnection.MusteriUniq,
                    remoteConnection.MusteriIp,
                    remoteConnection.BaglantiSaat,
                    remoteConnection.BaglantiTarih,
                    remoteConnection.BaglantiSure,
                    remoteConnection.BaglantiAciklama,
                    remoteConnection.BaglantiDestekNo,
                    remoteConnection.BaglantiUniq,
                    remoteConnection.MusteriErpId,
                    remoteConnection.DestekUrunId,
                    remoteConnection.TalepDetay,
                    remoteConnection.ToplamSure,
                    remoteConnection.ID
                });
                return affectedRows;
            }
        }
    }
}
