using Microsoft.Data.SqlClient;
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
        public async Task<List<DvDestekUrunler>> GetAllDvDestekUrunlerAsync()
        {
            var urunList = new List<DvDestekUrunler>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Dv_Destek_Urunler", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var urun = new DvDestekUrunler
                        {
                            Id = reader.GetInt32(0),
                            Urun = reader.GetString(1)
                        };
                        urunList.Add(urun);
                    }
                }
            }

            return urunList;
        }
        public async Task<IEnumerable<DvDestekMusteriUrun>> GetAllDvDestekMusteriUrunAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT Id, MusteriId, UrunId, BaslamaTarihi, SonKullanimTarihi, Aciklama, Miktar, Durum, BirimId FROM Dv_Destek_Musteri_Urun";
                return await connection.QueryAsync<DvDestekMusteriUrun>(query);
            }
        }




        public async Task<IEnumerable<DvDestekMusteriUrunDetay>> GetAllDvDestekMusteriUrunDetayAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT   BaglantiId = @BaglantiId, Tarih = @Tarih, Aciklama = @Aciklama, PersonelId = @PersonelId WHERE Id = @Id";
                return await connection.QueryAsync<DvDestekMusteriUrunDetay>(query);
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

        public async Task<IEnumerable<CallRecord>> GetCallsAsync(int page, int pageSize, string searchQuery, DateTime startDate, DateTime endDate, string selectedAranan)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var offset = (page - 1) * pageSize;

                var query = @"
            SELECT * FROM dbo.Dv_Destek_Cagrilar 
            WHERE (Tarih >= @StartDate AND Tarih <= @EndDate)
            AND (Aranan = @SelectedAranan OR @SelectedAranan IS NULL OR @SelectedAranan = '')
            AND (Arayan LIKE '%' + @SearchQuery + '%' OR @SearchQuery IS NULL OR @SearchQuery = '')
            ORDER BY Tarih DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                return await connection.QueryAsync<CallRecord>(query, new
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    SelectedAranan = selectedAranan,
                    SearchQuery = searchQuery,
                    Offset = offset,
                    PageSize = pageSize
                });
            }
        }

        public async Task<int> GetTotalCallCountAsync(DateTime startDate, DateTime endDate, string selectedAranan, string searchQuery)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
            SELECT COUNT(*) FROM dbo.Dv_Destek_Cagrilar 
            WHERE (Tarih >= @StartDate AND Tarih <= @EndDate)
            AND (Aranan = @SelectedAranan OR @SelectedAranan IS NULL OR @SelectedAranan = '')
            AND (Arayan LIKE '%' + @SearchQuery + '%' OR @SearchQuery IS NULL OR @SearchQuery = '')";

                return await connection.ExecuteScalarAsync<int>(query, new
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    SelectedAranan = selectedAranan,
                    SearchQuery = searchQuery
                });
            }
        }
        public async Task<IEnumerable<CallRecord>> GetAllCallsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM dbo.Dv_Destek_Cagrilar";
                return await connection.QueryAsync<CallRecord>(query);
            }
        }


        public async Task<IEnumerable<string>> GetUniqueArananListAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT DISTINCT Aranan FROM Dv_Destek_Cagrilar";
            return await connection.QueryAsync<string>(query);
        }


        public async Task<IEnumerable<RemoteConnection>> GetRemoteConnectionsAsync(int page, int pageSize, bool isAscending)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sortOrder = isAscending ? "ASC" : "DESC";
                var offset = (page - 1) * pageSize;

                var query = $@"
                    SELECT * FROM dbo.Dv_Destek_Uzak
                    ORDER BY ID {sortOrder}
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY";

                return await connection.QueryAsync<RemoteConnection>(query, new { Offset = offset, PageSize = pageSize });
            }
        }


       

        public async Task UpdateDvDestekMusteriUrunDetayAsync(DvDestekMusteriUrunDetay urunDetay)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE Dv_Destek_Musteri_Urun_Detay SET BaglantiId = @BaglantiId, Tarih = @Tarih, Aciklama = @Aciklama, PersonelId = @PersonelId WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@Id", urunDetay.Id);
                command.Parameters.AddWithValue("@BaglantiId", urunDetay.BaglantiId);
                command.Parameters.AddWithValue("@Tarih", urunDetay.Tarih);
                command.Parameters.AddWithValue("@Aciklama", urunDetay.Aciklama);
                command.Parameters.AddWithValue("@PersonelId", urunDetay.PersonelId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<DvDestekMusteriUrunDetay> GetDvDestekMusteriUrunDetayByIdAsync(int id)
        {
            DvDestekMusteriUrunDetay urunDetay = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT Id, BaglantiId, Tarih, Aciklama, PersonelId FROM Dv_Destek_Musteri_Urun_Detay WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        urunDetay = new DvDestekMusteriUrunDetay
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            BaglantiId = reader.GetInt32(reader.GetOrdinal("BaglantiId")),
                            Tarih = reader.GetDateTime(reader.GetOrdinal("Tarih")),
                            Aciklama = reader.GetString(reader.GetOrdinal("Aciklama")),
                            PersonelId = reader.GetInt32(reader.GetOrdinal("PersonelId"))
                        };
                    }
                }
            }
            return urunDetay;
        }

        public void UpdateDvDestekMusteriUrun(DvDestekMusteriUrun dvDestekMusteriUrun)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();


            using var command = new SqlCommand(UpdateDvDestekMusteriUrunQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekMusteriUrun.Id);
            command.Parameters.AddWithValue("@MusteriId", dvDestekMusteriUrun.MusteriId);
            command.Parameters.AddWithValue("@UrunId", dvDestekMusteriUrun.UrunId);
            command.Parameters.AddWithValue("@BaslamaTarihi", dvDestekMusteriUrun.BaslamaTarihi);
            command.Parameters.AddWithValue("@SonKullanimTarihi", dvDestekMusteriUrun.SonKullanimTarihi);
            command.Parameters.AddWithValue("@Aciklama", dvDestekMusteriUrun.Aciklama);
            command.Parameters.AddWithValue("@Miktar", dvDestekMusteriUrun.Miktar);
            command.Parameters.AddWithValue("@Durum", dvDestekMusteriUrun.Durum);
            command.Parameters.AddWithValue("@BirimId", dvDestekMusteriUrun.BirimId);
            command.ExecuteNonQuery();

        }
        public void UpdateDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();


            using var command = new SqlCommand(UpdateDvDestekMusteriUrunDetayQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekMusteriUrunDetay.Id);
            command.Parameters.AddWithValue("@MusteriId", dvDestekMusteriUrunDetay.BaglantiId);
            command.Parameters.AddWithValue("@UrunId", dvDestekMusteriUrunDetay.Tarih);
            command.Parameters.AddWithValue("@BaslamaTarihi", dvDestekMusteriUrunDetay.Aciklama);
            command.Parameters.AddWithValue("@SonKullanimTarihi", dvDestekMusteriUrunDetay.PersonelId);

            command.ExecuteNonQuery();

        }

        public async Task<int> GetTotalRemoteConnectionCountAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT COUNT(*) FROM dbo.Dv_Destek_Cagrilar";
                return await connection.ExecuteScalarAsync<int>(query);
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
                    Durum = short.TryParse(reader.GetString(reader.GetOrdinal("Durum")), out var durum) ? durum : default,
                    BagliFirmaId = reader.GetInt32(reader.GetOrdinal("BagliFirmaId")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama"))
                };

                kisilerList.Add(kisi);
            }

            return kisilerList;
        }


        public async Task<IEnumerable<CallRecord>> GetCallsAsync(int page, int pageSize, string searchQuery = "")
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"SELECT * FROM Dv_Destek_Cagrilar 
                          WHERE (@Search IS NULL OR Arayan LIKE @Search OR Aranan LIKE @Search)
                          ORDER BY Tarih
                          OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            return await connection.QueryAsync<CallRecord>(query, new
            {
                Search = string.IsNullOrEmpty(searchQuery) ? null : $"%{searchQuery}%",
                Offset = (page - 1) * pageSize,
                PageSize = pageSize
            });
        }
        private const string GetAllDvDestekMusteriUrunQuery = "SELECT * FROM Dv_Destek_Musteri_Urun";

        public List<DvDestekMusteriUrun> getAllDvDestekMusteriUrun()
        {
            var dvDestekMusteriUrun = new List<DvDestekMusteriUrun>();

            try
            {
                using var connection = DbConnector.GetConnection();
                connection.Open();

                using var command = new SqlCommand(GetAllDvDestekMusteriUrunQuery, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var dvDestekMusteriUrun1 = new DvDestekMusteriUrun
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        MusteriId = reader.GetInt32(reader.GetOrdinal("MusteriId")),
                        UrunId = reader.GetInt32(reader.GetOrdinal("UrunId")),
                        BaslamaTarihi = reader.GetDateTime(reader.GetOrdinal("BaslamaTarihi")),
                        SonKullanimTarihi = reader.GetDateTime(reader.GetOrdinal("SonKullanimTarihi")),
                        Aciklama = reader.GetString(reader.GetOrdinal("Aciklama")),
                        Miktar = reader.GetInt32(reader.GetOrdinal("Miktar")),
                        Durum = reader.GetString(reader.GetOrdinal("Durum")),
                        BirimId = reader.GetInt32(reader.GetOrdinal("BirimId"))
                    };
                    dvDestekMusteriUrun.Add(dvDestekMusteriUrun1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in getAllDvDestekMusteriUrun: {ex.Message}");
                // Loglama veya hata yönetimi eklenebilir
            }

            return dvDestekMusteriUrun;
        }


        public async Task<List<RemoteConnection>> SearchRemoteAsync(string search)
        {
            var remoteConnectionList = new List<RemoteConnection>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(
                @"SELECT * FROM Dv_Destek_Uzak 
          WHERE ID LIKE @Search 
             OR Baglanan LIKE @Search 
             OR Yon LIKE @Search 
             OR Musteri LIKE @Search 
            OR BaglantiAciklama LIKE @Search 

             ", connection);

            command.Parameters.AddWithValue("@Search", "%" + search + "%");

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var remoteConnection = new RemoteConnection
                {
                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                    Baglanan = reader.GetString(reader.GetOrdinal("Baglanan")),
                    BaglananUniq = reader.GetString(reader.GetOrdinal("BaglananUniq")),
                    BaglananIp = reader.GetString(reader.GetOrdinal("BaglananIp")),
                    Yon = reader.GetString(reader.GetOrdinal("Yon")),
                    Musteri = reader.GetString(reader.GetOrdinal("Musteri")),
                    MusteriUniq = reader.GetString(reader.GetOrdinal("MusteriUniq")),
                    MusteriIp = reader.GetString(reader.GetOrdinal("MusteriIp")),
                    BaglantiTarih = reader.GetDateTime(reader.GetOrdinal("BaglantiTarih")),
                    BaglantiAciklama = reader.GetString(reader.GetOrdinal("BaglantiAciklama")),


                };

                remoteConnectionList.Add(remoteConnection);
            }

            return remoteConnectionList;
        }


        public async Task<List<DvDestekMusteriUrun>> SearchUrunAsync(string search)
        {
            var urunList = new List<DvDestekMusteriUrun>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT * FROM Dv_Destek_Musteri_Urun WHERE Id LIKE @Search OR Aciklama LIKE @Search ;", connection);
            command.Parameters.AddWithValue("@Search", "%" + search + "%");

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var urun = new DvDestekMusteriUrun
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),

                    MusteriId = reader.GetInt32(reader.GetOrdinal("MusteriId")),
                    UrunId = reader.GetInt32(reader.GetOrdinal("UrunId")),
                    BaslamaTarihi = reader.GetDateTime(reader.GetOrdinal("BaslamaTarihi")),
                    SonKullanimTarihi = reader.GetDateTime(reader.GetOrdinal("SonKullanimTarihi")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama")),

                    Miktar = reader.GetInt32(reader.GetOrdinal("Miktar")),
                    Durum = reader.GetString(reader.GetOrdinal("Durum")),
                    BirimId = reader.GetInt32(reader.GetOrdinal("BirimId")),

                };

                urunList.Add(urun);
            }

            return urunList;
        }
     

        private const string UpdateDvDestekPersonelQuery =
         @"UPDATE Dv.dbo.Dv_Destek_Personel 
          SET Ad_Soyad = @Ad_Soyad, 
              E_posta = @EPosta, 
              Sifre = @Sifre, 
              Telefon = @Telefon, 
              Dahili = @Dahili 
          WHERE Id = @Id";

        public async Task UpdateDvDestekPersonelAsync(DvDestekPersonel dvDestekPersonel)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(UpdateDvDestekPersonelQuery, new
                {
                    dvDestekPersonel.Id,
                    dvDestekPersonel.Ad_Soyad,
                    EPosta = dvDestekPersonel.E_posta, // SQL parametresi ile eşleşti
                    dvDestekPersonel.Sifre,
                    dvDestekPersonel.Telefon,
                    dvDestekPersonel.Dahili
                });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw; // Re-throw the exception if you want it to propagate
            }
        }

        private const string UpdateDvDestekMusteriUrunQuery =
        @"UPDATE Dv_Destek_Musteri_Urun SET MusteriId = @MusteriId, UrunId = @UrunId, BaslamaTarihi = @BaslamaTarihi, SonKullanimTarihi = @SonKullanimTarihi, Aciklama = @Aciklama, Miktar = @Miktar, Durum = @Durum, BirimId = @BirimId WHERE Id = @Id";

        private const string UpdateDvDestekMusteriUrunDetayQuery =
         @"UPDATE Dv_Destek_Musteri_Urun_Detay SET BaglantiId = @BaglantiId, Tarih = @Tarih, Aciklama = @Aciklama, PersonelId = @PersonelId WHERE Id = @Id";
    }

    }

