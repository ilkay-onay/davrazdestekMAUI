﻿using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Common;
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
                var query = "SELECT COUNT(*) FROM Dv_Destek_Personel WHERE [E_posta] = @Email AND [Sifre] = @Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    var result = (int)await command.ExecuteScalarAsync();
                    return result > 0;
                }
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

                    var calls = await connection.QueryAsync<CallRecord>(query, new { RowStart = rowStart, RowEnd = rowEnd });
                    return calls;
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

                    var remoteConnections = await connection.QueryAsync<RemoteConnection>(query, new { RowStart = rowStart, RowEnd = rowEnd });
                    return remoteConnections;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetRemoteConnectionsAsync: {ex.Message}");
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
        public async Task UpdateKisilerAsync(Kisiler kisi)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Dv_Destek_Kisiler 
                            SET Ad_Soyad = @Ad_Soyad, Gorev = @Gorev, Mail = @Mail, Telefon = @Telefon, Durum = @Durum, BagliFirmaId = @BagliFirmaId, Aciklama = @Aciklama 
                            WHERE Id = @Id";
                await connection.ExecuteAsync(sql, kisi);
            }
        }
        public async Task<DvDestekPersonel> GetUserInfo(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Dv_Destek_Personel WHERE [E_posta] = @Email AND [Sifre] = @Password";
                return await connection.QuerySingleOrDefaultAsync<DvDestekPersonel>(query, new { Email = email, Password = password });
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
        public async Task<IEnumerable<Kisiler>> GetKisilerAsync(int pageNumber, int pageSize)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = @"
            SELECT * FROM (
                SELECT 
                    ROW_NUMBER() OVER (ORDER BY Id ASC) AS RowNum,
                    Id, Ad_Soyad, Gorev, Mail, Telefon, Durum, BagliFirmaId, Aciklama
                FROM Dv_Destek_Kisiler
            ) AS RowConstrainedResult
            WHERE RowNum >= @RowStart
                AND RowNum < @RowEnd
            ORDER BY RowNum";

                    int rowStart = (pageNumber - 1) * pageSize + 1;
                    int rowEnd = pageNumber * pageSize + 1;

                    var kisiler = await connection.QueryAsync<Kisiler>(query, new { RowStart = rowStart, RowEnd = rowEnd });
                    return kisiler;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetKisilerAsync: {ex.Message}");
                throw;
            }
        }
   

        public async Task<int> GetTotalKisilerCountAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT COUNT(*) FROM Dv_Destek_Kisiler";
                    return await connection.ExecuteScalarAsync<int>(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTotalKisilerCountAsync: {ex.Message}");
                throw;
            }
        }
        public async Task<List<Kisiler>> SearchKisilerAsync(string search)
        {
            var kisilerList = new List<Kisiler>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT * FROM Dv_Destek_Kisiler WHERE Ad_Soyad LIKE @Search", connection);
            command.Parameters.AddWithValue("@Search", "%" + search + "%");

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var kisi = new Kisiler
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Ad_Soyad = reader.GetString(reader.GetOrdinal("Ad_Soyad")),
                    Gorev = reader.GetString(reader.GetOrdinal("Gorev")),
                    Mail = reader.GetString(reader.GetOrdinal("Mail")),
                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                    Durum = reader.GetInt16(reader.GetOrdinal("Durum")),
                    BagliFirmaId = reader.GetInt32(reader.GetOrdinal("BagliFirmaId")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama"))
                };

                kisilerList.Add(kisi);
            }

            return kisilerList;
        }


    }
}

