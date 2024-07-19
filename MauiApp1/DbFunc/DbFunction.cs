using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace  MauiApp1.dvFunction
{
    public class DbFunction : IDbFunctions
    {
        //DvDestekCagrilar
        private const string InsertDvDestekCagrilarQuery =
            @"INSERT INTO Dv_Destek_Cagrilar (Tarih, Arayan, Aranan, ToplamSure, BeklemeSuresi, GorusmeSuresi, Sonuc, Tipi) 
                      VALUES (@Tarih, @Arayan, @Aranan, @ToplamSure, @BeklemeSuresi, @GorusmeSuresi, @Sonuc, @Tipi)";

        private const string UpdateDvDestekCagrilarQuery = @"UPDATE Dv_Destek_Cagrilar 
                      SET Tarih = @Tarih, Arayan = @Arayan, Aranan = @Aranan, ToplamSure = @ToplamSure, BeklemeSuresi = @BeklemeSuresi, GorusmeSuresi = @GorusmeSuresi, Sonuc = @Sonuc, Tipi = @Tipi
                      WHERE Id = @Id";

        private const string DaleteDvDestekCagrilarQuery = "DELETE FROM Dv_Destek_Cagrilar WHERE Id = @Id";
        private const string GetAllDvDestekCagrilarQuery = "SELECT * FROM Dv_Destek_Cagrilar";

        private const string SearchDvDestekCagrilarQuery =
            "SELECT * FROM Dv_Destek_Cagrilar WHERE Arayan LIKE @Search OR Aranan LIKE @Search";

        private const string GetByDestekCagrilarIdQuery = "SELECT * FROM Dv_Destek_Cagrilar WHERE Id = @Id";

        //DvDestekKisiler
        private const string InsertDvDestekKisilerQuery =
            @"INSERT INTO Dv_Destek_Kisiler (Ad_Soyad, Gorev, Mail, Telefon, Durum, BagliFirmaId, Aciklama) VALUES (@Ad_Soyad, @Gorev, @Mail, @Telefon, @Durum, @BagliFirmaId, @Aciklama)";

        private const string UpdateDvDestekKisilerQuery =
            @"UPDATE Dv_Destek_Kisiler SET Ad_Soyad = @Ad_Soyad, Gorev = @Gorev, Mail = @Mail, Telefon = @Telefon, Durum = @Durum, BagliFirmaId = @BagliFirmaId, Aciklama = @Aciklama WHERE Id = @Id";

        private const string DeleteDvDestekKisilerQuery = "DELETE FROM Dv_Destek_Kisiler WHERE Id = @Id";
        private const string GetAllDvDestekKisilerQuery = "SELECT * FROM Dv_Destek_Kisiler";

        private const string GetSearchDvDestekKisilerQuery =
            "SELECT * FROM Dv_Destek_Kisiler WHERE Ad_Soyad LIKE @Search OR Gorev LIKE @Search OR Mail LIKE @Search OR Telefon LIKE @Search OR Aciklama LIKE @Search";

        private const string GetDvDestekKisilerByIdQuery = "SELECT * FROM Dv_Destek_Kisiler WHERE Id = @Id";

        //DvDestekMusteriUrun
        private const string InsertDvDestekMusteriUrunQuery =
            @"INSERT INTO Dv_Destek_Musteri_Urun (Id,MusteriId, UrunId,BaslamaTarihi,SonKullanimTarihi,Aciklama,Miktar,Durum,BirimId) VALUES (@Id,@MusteriId, @UrunId, @BaslamaTarihi, @SonKullanimTarihi, @Aciklama, @Miktar, @Durum, @BirimId)";

        private const string UpdateDvDestekMusteriUrunQuery =
            @"UPDATE Dv_Destek_Musteri_Urun SET MusteriId = @MusteriId, UrunId = @UrunId, BaslamaTarihi = @BaslamaTarihi, SonKullanimTarihi = @SonKullanimTarihi, Aciklama = @Aciklama, Miktar = @Miktar, Durum = @Durum, BirimId = @BirimId WHERE Id = @Id";

        private const string DeleteDvDestekMusteriUrunQuery = "DELETE FROM Dv_Destek_Musteri_Urun WHERE Id = @Id";
        private const string GetAllDvDestekMusteriUrunQuery = "SELECT * FROM Dv_Destek_Musteri_Urun";

        private const string GetSearchDvDestekMusteriUrunQuery =
            "SELECT * FROM Dv_Destek_Musteri_Urun WHERE Aciklama LIKE @Search";

        private const string GetDvDestekMusteriUrunByIdQuery = "SELECT * FROM Dv_Destek_Musteri_Urun WHERE Id = @Id";


        //DvDestekMusteriUrunDetay
        private const string InsertDvDestekMusteriUrunDetayQuery =
            @"INSERT INTO Dv_Destek_Musteri_Urun_Detay (BaglantiId,Tarih,Aciklama,PersonelId) VALUES (@BaglantiId,@Tarih, @Aciklama, @PersonelId)";

        private const string UpdateDvDestekMusteriUrunDetayQuery =
            @"UPDATE Dv_Destek_Musteri_Urun_Detay SET BaglantiId = @BaglantiId, Tarih = @Tarih, Aciklama = @Aciklama, PersonelId = @PersonelId WHERE Id = @Id";

        private const string DeleteDvDestekMusteriUrunDetayQuery =
            "DELETE FROM Dv_Destek_Musteri_Urun_Detay WHERE Id = @Id";

        private const string GetAllDvDestekMusteriUrunDetayQuery = "SELECT * FROM Dv_Destek_Musteri_Urun_Detay";

        private const string GetSearchDvDestekMusteriUrunDetayQuery =
            "SELECT * FROM Dv_Destek_Musteri_Urun_Detay WHERE Aciklama LIKE @Search";

        private const string GetDvDestekMusteriUrunDetayByIdQuery =
            "SELECT * FROM Dv_Destek_Musteri_Urun_Detay WHERE Id = @Id";



        //DvDestekPersonel
        private const string InsertDvDestekPersonelQuery =
            @"INSERT INTO Dv_Destek_Personel (Ad_Soyad,[E-posta],Sifre,Telefon,Dahili) VALUES (@Ad_Soyad,@Eposta, @Sifre, @Telefon, @Dahili)";

        private const string UpdateDvDestekPersonelQuery =
            @"UPDATE Dv_Destek_Personel SET Ad_Soyad = @Ad_Soyad, [E-Posta] = @EPosta, Sifre = @Sifre, Telefon = @Telefon, Dahili = @Dahili WHERE Id = @Id";

        private const string DeleteDvDestekPersonelQuery = "DELETE FROM Dv_Destek_Personel WHERE Id = @Id";
        private const string GetAllDvDestekPersonelQuery = "SELECT * FROM Dv_Destek_Personel";

        private const string GetSearchDvDestekPersonelQuery =
            "SELECT * FROM Dv_Destek_Personel WHERE Ad_Soyad LIKE @Search OR [E-Posta] LIKE @Search OR Telefon LIKE @Search";

        private const string GetDvDestekPersonelByIdQuery = "SELECT * FROM Dv_Destek_Personel WHERE Id = @Id";


        //DvDestekUrunler
        private const string InsertDvDestekUrunlerQuery = @"INSERT INTO Dv_Destek_Urunler (Urun) VALUES (@Urun)";
        private const string UpdateDvDestekUrunlerQuery = @"UPDATE Dv_Destek_Urunler SET Urun = @Urun WHERE Id = @Id";
        private const string DeleteDvDestekUrunlerQuery = "DELETE FROM Dv_Destek_Urunler WHERE Id = @Id";
        private const string GetAllDvDestekUrunlerQuery = "SELECT * FROM Dv_Destek_Urunler";
        private const string GetSearchDvDestekUrunlerQuery = "SELECT * FROM Dv_Destek_Urunler WHERE Urun LIKE @Search";
        private const string GetDvDestekUrunlerByIdQuery = "SELECT * FROM Dv_Destek_Urunler WHERE Id = @Id";


        //DvDestekUzak
        private const string InsertDvDestekUzakQuery =
            @"INSERT INTO Dv_Destek_Uzak (Baglanan, BaglananUniq, BaglananIp, Yon, Musteri, MusteriUniq, MusteriIp, BaglantiSaat, BaglantiTarih, BaglantiSure, BaglantiAciklama, BaglantiDestekNo, BaglantiUniq, MusteriErpId, DestekUrunId, TalepDetay, ToplamSure) VALUES (@Baglanan, @BaglananUniq, @BaglananIp, @Yon, @Musteri, @MusteriUniq, @MusteriIp, @BaglantiSaat, @BaglantiTarih, @BaglantiSure, @BaglantiAciklama, @BaglantiDestekNo, @BaglantiUniq, @MusteriErpId, @DestekUrunId, @TalepDetay, @ToplamSure)";

        private const string UpdateDvDestekUzakQuery =
            @"UPDATE Dv_Destek_Uzak SET Baglanan = @Baglanan, BaglananUniq = @BaglananUniq, BaglananIp = @BaglananIp, Yon = @Yon, Musteri = @Musteri, MusteriUniq = @MusteriUniq, MusteriIp = @MusteriIp, BaglantiSaat = @BaglantiSaat, BaglantiTarih = @BaglantiTarih, BaglantiSure = @BaglantiSure, BaglantiAciklama = @BaglantiAciklama, BaglantiDestekNo = @BaglantiDestekNo, BaglantiUniq = @BaglantiUniq, MusteriErpId = @MusteriErpId, DestekUrunId = @DestekUrunId, TalepDetay = @TalepDetay, ToplamSure = @ToplamSure WHERE Id = @Id";

        private const string DeleteDvDestekUzakQuery = "DELETE FROM Dv_Destek_Uzak WHERE Id = @Id";
        private const string GetAllDvDestekUzakQuery = "SELECT * FROM Dv_Destek_Uzak";

        private const string GetSearchDvDestekUzakQuery =
            "SELECT * FROM Dv_Destek_Uzak WHERE Baglanan LIKE @Search OR Musteri LIKE @Search OR BaglantiAciklama LIKE @Search OR TalepDetay LIKE @Search";

        private const string GetDvDestekUzakByIdQuery = "SELECT * FROM Dv_Destek_Uzak WHERE Id = @Id";



        //DvDestekUzakDetay
        private const string InsertDvDestekUzakDetayQuery =
            @"INSERT INTO Dv_Destek_Uzak_Detay(Id,DestekId,PersonelId,YapilanIslem,IslemTarihi,DestekVerilenKisi) VALUES (@Id,@DestekId, @PersonelId, @YapilanIslem, @IslemTarihi, @DestekVerilenKisi)";

        private const string UpdateDvDestekUzakDetayQuery =
            @"UPDATE Dv_Destek_Uzak_Detay SET DestekId = @DestekId, PersonelId = @PersonelId, YapilanIslem = @YapilanIslem, IslemTarihi = @IslemTarihi, DestekVerilenKisi = @DestekVerilenKisi WHERE Id = @Id";

        private const string DeleteDvDestekUzakDetayQuery = "DELETE FROM Dv_Destek_Uzak_Detay WHERE Id = @Id";
        private const string GetAllDvDestekUzakDetayQuery = "SELECT * FROM Dv_Destek_Uzak_Detay";

        private const string GetSearchDvDestekUzakDetayQuery =
            "SELECT * FROM Dv_Destek_Uzak_Detay WHERE YapilanIslem LIKE @Search OR DestekVerilenKisi LIKE @Search";

        private const string GetDvDestekUzakDetayByIdQuery = "SELECT * FROM Dv_Destek_Uzak_Detay WHERE Id = @Id";





        // DvDestekCagrilar
        public void InsertDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar)
        {
            using var connection = DbConnector.GetConnection();


            using var command = new SqlCommand(InsertDvDestekCagrilarQuery, connection);
            command.Parameters.AddWithValue("@Tarih", dvDestekCagrilar.Tarih);
            command.Parameters.AddWithValue("@Arayan", dvDestekCagrilar.Arayan);
            command.Parameters.AddWithValue("@Aranan", dvDestekCagrilar.Aranan);
            command.Parameters.AddWithValue("@ToplamSure", dvDestekCagrilar.ToplamSure);
            command.Parameters.AddWithValue("@BeklemeSuresi", dvDestekCagrilar.BeklemeSuresi);
            command.Parameters.AddWithValue("@GorusmeSuresi", dvDestekCagrilar.GorusmeSuresi);
            command.Parameters.AddWithValue("@Sonuc", dvDestekCagrilar.Sonuc);
            command.Parameters.AddWithValue("@Tipi", dvDestekCagrilar.Tipi);

            command.ExecuteNonQuery();
        }

        public void UpdateDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar)
        {
            using var connection = DbConnector.GetConnection();
            ;
            using var command = new SqlCommand(UpdateDvDestekCagrilarQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekCagrilar.Id);
            command.Parameters.AddWithValue("@Tarih", dvDestekCagrilar.Tarih);
            command.Parameters.AddWithValue("@Arayan", dvDestekCagrilar.Arayan);
            command.Parameters.AddWithValue("@Aranan", dvDestekCagrilar.Aranan);
            command.Parameters.AddWithValue("@ToplamSure", dvDestekCagrilar.ToplamSure);
            command.Parameters.AddWithValue("@BeklemeSuresi", dvDestekCagrilar.BeklemeSuresi);
            command.Parameters.AddWithValue("@GorusmeSuresi", dvDestekCagrilar.GorusmeSuresi);
            command.Parameters.AddWithValue("@Sonuc", dvDestekCagrilar.Sonuc);
            command.Parameters.AddWithValue("@Tipi", dvDestekCagrilar.Tipi);
            command.ExecuteNonQuery();
        }

        public void DeleteDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(DaleteDvDestekCagrilarQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekCagrilar.Id);
            command.ExecuteNonQuery();
        }

        public List<DvDestekCagrilar> SearchDvDestekCagrilar(string search)
        {
            var dvDestekCagrilarList = new List<DvDestekCagrilar>();
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(SearchDvDestekCagrilarQuery, connection);

            command.Parameters.AddWithValue("@Search", "%" + search + "%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekCagrilar = new DvDestekCagrilar
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Tarih = reader.GetDateTime(reader.GetOrdinal("tarih")),
                    Arayan = reader.GetString(reader.GetOrdinal("arayan")),
                    Aranan = reader.GetString(reader.GetOrdinal("aranan")),
                    ToplamSure = reader.GetTimeSpan(reader.GetOrdinal("toplamSure")),
                    BeklemeSuresi = reader.GetTimeSpan(reader.GetOrdinal("beklemeSuresi")),
                    GorusmeSuresi = reader.GetTimeSpan(reader.GetOrdinal("gorusmeSuresi")),
                    Sonuc = reader.GetInt32(reader.GetOrdinal("sonuc")),
                    Tipi = reader.GetInt16(reader.GetOrdinal("tipi"))
                };

                dvDestekCagrilarList.Add(dvDestekCagrilar);
            }

            return dvDestekCagrilarList;
        }

        public DvDestekCagrilar GetDvDestekCagrilarById(int id)
        {
            using var connection = DbConnector.GetConnection();
            ;
            using var command = new SqlCommand(GetByDestekCagrilarIdQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var dvDestekCagrilar = new DvDestekCagrilar
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Tarih = reader.GetDateTime(reader.GetOrdinal("tarih")),
                    Arayan = reader.GetString(reader.GetOrdinal("arayan")),
                    Aranan = reader.GetString(reader.GetOrdinal("aranan")),
                    ToplamSure = reader.GetTimeSpan(reader.GetOrdinal("toplamSure")),
                    BeklemeSuresi = reader.GetTimeSpan(reader.GetOrdinal("beklemeSuresi")),
                    GorusmeSuresi = reader.GetTimeSpan(reader.GetOrdinal("gorusmeSuresi")),
                    Sonuc = reader.GetInt32(reader.GetOrdinal("sonuc")),
                    Tipi = reader.GetInt16(reader.GetOrdinal("tipi"))
                };

                return dvDestekCagrilar;
            }

            return null;
        }

        public List<DvDestekCagrilar> GetAllDvDestekCagrilar()
        {
            var dvDestekCagrilarList = new List<DvDestekCagrilar>();

            using var connection = DbConnector.GetConnection();


            using var command = new SqlCommand(GetAllDvDestekCagrilarQuery, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekCagrilar = new DvDestekCagrilar
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Tarih = reader.GetDateTime(reader.GetOrdinal("tarih")),
                    Arayan = reader.GetString(reader.GetOrdinal("arayan")),
                    Aranan = reader.GetString(reader.GetOrdinal("aranan")),
                    ToplamSure = reader.GetTimeSpan(reader.GetOrdinal("toplamSure")),
                    BeklemeSuresi = reader.GetTimeSpan(reader.GetOrdinal("beklemeSuresi")),
                    GorusmeSuresi = reader.GetTimeSpan(reader.GetOrdinal("gorusmeSuresi")),
                    Sonuc = reader.GetInt32(reader.GetOrdinal("sonuc")),
                    Tipi = reader.GetInt16(reader.GetOrdinal("tipi"))
                };

                dvDestekCagrilarList.Add(dvDestekCagrilar);
            }

            return dvDestekCagrilarList;
        }

        // DvDestekKisiler

        public void insertDvDestekKisiler(DvDestekKisiler dvDestekKisiler)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(InsertDvDestekKisilerQuery, connection);
            command.Parameters.AddWithValue("@Ad_Soyad", dvDestekKisiler.AdSoyad);
            command.Parameters.AddWithValue("@Gorev", dvDestekKisiler.Gorev);
            command.Parameters.AddWithValue("@Mail", dvDestekKisiler.Mail);
            command.Parameters.AddWithValue("@Telefon", dvDestekKisiler.Telefon);
            command.Parameters.AddWithValue("@Durum", dvDestekKisiler.Durum);
            command.Parameters.AddWithValue("@BagliFirmaId", dvDestekKisiler.BagliFirmaId);
            command.Parameters.AddWithValue("@Aciklama", dvDestekKisiler.Aciklama);
            command.ExecuteNonQuery();
        }

        public void updateDvDestekKisiler(DvDestekKisiler dvDestekKisiler)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(UpdateDvDestekKisilerQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekKisiler.Id);
            command.Parameters.AddWithValue("@Ad_Soyad", dvDestekKisiler.AdSoyad);
            command.Parameters.AddWithValue("@Gorev", dvDestekKisiler.Gorev);
            command.Parameters.AddWithValue("@Mail", dvDestekKisiler.Mail);
            command.Parameters.AddWithValue("@Telefon", dvDestekKisiler.Telefon);
            command.Parameters.AddWithValue("@Durum", dvDestekKisiler.Durum);
            command.Parameters.AddWithValue("@BagliFirmaId", dvDestekKisiler.BagliFirmaId);
            command.Parameters.AddWithValue("@Aciklama", dvDestekKisiler.Aciklama);
            command.ExecuteNonQuery();
        }

        public void deleteDvDestekKisiler(DvDestekKisiler dvDestekKisiler)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(DeleteDvDestekKisilerQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekKisiler.Id);
            command.ExecuteNonQuery();
        }

        public List<DvDestekKisiler> getAllDvDestekKisiler()
        {
            var dvDestekKisiler = new List<DvDestekKisiler>();

            using var connection = DbConnector.GetConnection();


            using var command = new SqlCommand(GetAllDvDestekKisilerQuery, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekKisiler1 = new DvDestekKisiler
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    AdSoyad = reader.GetString(reader.GetOrdinal("Ad_Soyad")),
                    Gorev = reader.GetString(reader.GetOrdinal("Gorev")),
                    Mail = reader.GetString(reader.GetOrdinal("Mail")),
                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                    Durum = reader.GetInt16(reader.GetOrdinal("Durum")),
                    BagliFirmaId = reader.GetInt32(reader.GetOrdinal("BagliFirmaId")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama"))
                };

                dvDestekKisiler.Add(dvDestekKisiler1);
            }

            return dvDestekKisiler;
        }

        public List<DvDestekKisiler> searchDvDestekKisiler(string search)
        {
            var dvDestekKisilerList = new List<DvDestekKisiler>();
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetSearchDvDestekKisilerQuery, connection);

            command.Parameters.AddWithValue("@Search", "%" + search + "%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekKisiler = new DvDestekKisiler
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    AdSoyad = reader.GetString(reader.GetOrdinal("Ad_Soyad")),
                    Gorev = reader.GetString(reader.GetOrdinal("Gorev")),
                    Mail = reader.GetString(reader.GetOrdinal("Mail")),
                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                    Durum = reader.GetInt16(reader.GetOrdinal("Durum")),
                    BagliFirmaId = reader.GetInt32(reader.GetOrdinal("BagliFirmaId")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama"))
                };

                dvDestekKisilerList.Add(dvDestekKisiler);
            }

            return dvDestekKisilerList;
        }

        public DvDestekKisiler getDvDestekKisilerById(int id)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetDvDestekKisilerByIdQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var dvDestekKisiler = new DvDestekKisiler
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    AdSoyad = reader.GetString(reader.GetOrdinal("Ad_Soyad")),
                    Gorev = reader.GetString(reader.GetOrdinal("Gorev")),
                    Mail = reader.GetString(reader.GetOrdinal("Mail")),
                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                    Durum = reader.GetInt16(reader.GetOrdinal("Durum")),
                    BagliFirmaId = reader.GetInt32(reader.GetOrdinal("BagliFirmaId")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama"))
                };

                return dvDestekKisiler;
            }

            return null;

        }


        //DvDestekMusteriUrun

        public void insertDvDestekMusteriUrun(DvDestekMusteriUrun dvDestekMusteriUrun)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(InsertDvDestekMusteriUrunQuery, connection);
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


        public void updateDvDestekMusteriUrun(DvDestekMusteriUrun dvDestekMusteriUrun)
        {
            using var connection = DbConnector.GetConnection();
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

        public void deleteDvDestekMusteriUrun(DvDestekMusteriUrun dvDestekMusteriUrun)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(DeleteDvDestekMusteriUrunQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekMusteriUrun.Id);
            command.ExecuteNonQuery();
        }

        public List<DvDestekMusteriUrun> getAllDvDestekMusteriUrun()
        {
            var dvDestekMusteriUrun = new List<DvDestekMusteriUrun>();

            using var connection = DbConnector.GetConnection();

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
                    Durum = reader.GetInt16(reader.GetOrdinal("Durum")),
                    BirimId = reader.GetInt32(reader.GetOrdinal("BirimId"))
                };
                dvDestekMusteriUrun.Add(dvDestekMusteriUrun1);
            }

            return dvDestekMusteriUrun;

        }

        public List<DvDestekMusteriUrun> searchDvDestekMusteriUrun(string search)
        {
            var dvDestekMusteriUrunList = new List<DvDestekMusteriUrun>();
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetSearchDvDestekMusteriUrunQuery, connection);

            command.Parameters.AddWithValue("@Search", "%" + search + "%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekMusteriUrun = new DvDestekMusteriUrun
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    MusteriId = reader.GetInt32(reader.GetOrdinal("MusteriId")),
                    UrunId = reader.GetInt32(reader.GetOrdinal("UrunId")),
                    BaslamaTarihi = reader.GetDateTime(reader.GetOrdinal("BaslamaTarihi")),
                    SonKullanimTarihi = reader.GetDateTime(reader.GetOrdinal("SonKullanimTarihi")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama")),
                    Miktar = reader.GetInt32(reader.GetOrdinal("Miktar")),
                    Durum = reader.GetInt16(reader.GetOrdinal("Durum")),
                    BirimId = reader.GetInt32(reader.GetOrdinal("BirimId"))
                };

                dvDestekMusteriUrunList.Add(dvDestekMusteriUrun);
            }

            return dvDestekMusteriUrunList;

        }

        public DvDestekMusteriUrun getDvDestekMusteriUrunById(int id)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetDvDestekMusteriUrunByIdQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var dvDestekMusteriUrun = new DvDestekMusteriUrun
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    MusteriId = reader.GetInt32(reader.GetOrdinal("MusteriId")),
                    UrunId = reader.GetInt32(reader.GetOrdinal("UrunId")),
                    BaslamaTarihi = reader.GetDateTime(reader.GetOrdinal("BaslamaTarihi")),
                    SonKullanimTarihi = reader.GetDateTime(reader.GetOrdinal("SonKullanimTarihi")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama")),
                    Miktar = reader.GetInt32(reader.GetOrdinal("Miktar")),
                    Durum = reader.GetInt16(reader.GetOrdinal("Durum")),
                    BirimId = reader.GetInt32(reader.GetOrdinal("BirimId"))
                };

                return dvDestekMusteriUrun;
            }

            return null;

        }


        //DvDestekMusteriUrunDetay
        public void insertDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(InsertDvDestekMusteriUrunDetayQuery, connection);
            command.Parameters.AddWithValue("@BaglantiId", dvDestekMusteriUrunDetay.BaglantiId);
            command.Parameters.AddWithValue("@Tarih", dvDestekMusteriUrunDetay.Tarih);
            command.Parameters.AddWithValue("@Aciklama", dvDestekMusteriUrunDetay.Aciklama);
            command.Parameters.AddWithValue("@PersonelId", dvDestekMusteriUrunDetay.PersonelId);
            command.ExecuteNonQuery();
        }

        public void updateDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay)
        {

            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(UpdateDvDestekMusteriUrunDetayQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekMusteriUrunDetay.Id);
            command.Parameters.AddWithValue("@BaglantiId", dvDestekMusteriUrunDetay.BaglantiId);
            command.Parameters.AddWithValue("@Tarih", dvDestekMusteriUrunDetay.Tarih);
            command.Parameters.AddWithValue("@Aciklama", dvDestekMusteriUrunDetay.Aciklama);
            command.Parameters.AddWithValue("@PersonelId", dvDestekMusteriUrunDetay.PersonelId);
            command.ExecuteNonQuery();



        }

        public void deleteDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(DeleteDvDestekMusteriUrunDetayQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekMusteriUrunDetay.Id);
            command.ExecuteNonQuery();
        }

        public List<DvDestekMusteriUrunDetay> getAllDvDestekMusteriUrunDetay()
        {
            var dvDestekMusteriUrunDetay = new List<DvDestekMusteriUrunDetay>();

            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetAllDvDestekMusteriUrunDetayQuery, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekMusteriUrunDetay1 = new DvDestekMusteriUrunDetay
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    BaglantiId = reader.GetInt32(reader.GetOrdinal("BaglantiId")),
                    Tarih = reader.GetDateTime(reader.GetOrdinal("Tarih")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama")),
                    PersonelId = reader.GetInt32(reader.GetOrdinal("PersonelId"))
                };
                dvDestekMusteriUrunDetay.Add(dvDestekMusteriUrunDetay1);
            }

            return dvDestekMusteriUrunDetay;

        }

        public List<DvDestekMusteriUrunDetay> searchDvDestekMusteriUrunDetay(string search)
        {
            var dvDestekMusteriUrunDetayList = new List<DvDestekMusteriUrunDetay>();
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetSearchDvDestekMusteriUrunDetayQuery, connection);

            command.Parameters.AddWithValue("@Search", "%" + search + "%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekMusteriUrunDetay = new DvDestekMusteriUrunDetay
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    BaglantiId = reader.GetInt32(reader.GetOrdinal("BaglantiId")),
                    Tarih = reader.GetDateTime(reader.GetOrdinal("Tarih")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama")),
                    PersonelId = reader.GetInt32(reader.GetOrdinal("PersonelId"))
                };

                dvDestekMusteriUrunDetayList.Add(dvDestekMusteriUrunDetay);
            }

            return dvDestekMusteriUrunDetayList;

        }

        public DvDestekMusteriUrunDetay getDvDestekMusteriUrunDetayById(int id)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetDvDestekMusteriUrunDetayByIdQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var dvDestekMusteriUrunDetay = new DvDestekMusteriUrunDetay
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    BaglantiId = reader.GetInt32(reader.GetOrdinal("BaglantiId")),
                    Tarih = reader.GetDateTime(reader.GetOrdinal("Tarih")),
                    Aciklama = reader.GetString(reader.GetOrdinal("Aciklama")),
                    PersonelId = reader.GetInt32(reader.GetOrdinal("PersonelId"))
                };

                return dvDestekMusteriUrunDetay;
            }

            return null;

        }

        //DvDestekPersonel

        public void insertDvDestekPersonel(DvDestekPersonel dvDestekPersonel)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(InsertDvDestekPersonelQuery, connection);
            command.Parameters.AddWithValue("@Ad_Soyad", dvDestekPersonel.AdSoyad);
            command.Parameters.AddWithValue("@EPosta", dvDestekPersonel.Eposta);
            command.Parameters.AddWithValue("@Sifre", dvDestekPersonel.Sifre);
            command.Parameters.AddWithValue("@Telefon", dvDestekPersonel.Telefon);
            command.Parameters.AddWithValue("@Dahili", dvDestekPersonel.Dahili);
            command.ExecuteNonQuery();
        }

        public void updateDvDestekPersonel(DvDestekPersonel dvDestekPersonel)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(UpdateDvDestekPersonelQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekPersonel.Id);
            command.Parameters.AddWithValue("@Ad_Soyad", dvDestekPersonel.AdSoyad);
            command.Parameters.AddWithValue("@EPosta", dvDestekPersonel.Eposta);
            command.Parameters.AddWithValue("@Sifre", dvDestekPersonel.Sifre);
            command.Parameters.AddWithValue("@Telefon", dvDestekPersonel.Telefon);
            command.Parameters.AddWithValue("@Dahili", dvDestekPersonel.Dahili);
            command.ExecuteNonQuery();

        }

        public void deleteDvDestekPersonel(DvDestekPersonel dvDestekPersonel)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(DeleteDvDestekPersonelQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekPersonel.Id);
            command.ExecuteNonQuery();

        }

        public List<DvDestekPersonel> getAllDvDestekPersonel()
        {
            var dvDestekPersonel = new List<DvDestekPersonel>();

            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetAllDvDestekPersonelQuery, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekPersonel1 = new DvDestekPersonel
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    AdSoyad = reader.GetString(reader.GetOrdinal("Ad_Soyad")),
                    Eposta = reader.GetString(reader.GetOrdinal("E-Posta")),
                    Sifre = reader.GetString(reader.GetOrdinal("Sifre")),
                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                    Dahili = reader.GetString(reader.GetOrdinal("Dahili"))
                };
                dvDestekPersonel.Add(dvDestekPersonel1);
            }

            return dvDestekPersonel;

        }

        public List<DvDestekPersonel> searchDvDestekPersonel(string search)
        {
            var dvDestekPersonelList = new List<DvDestekPersonel>();
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetSearchDvDestekPersonelQuery, connection);

            command.Parameters.AddWithValue("@Search", "%" + search + "%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekPersonel = new DvDestekPersonel
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    AdSoyad = reader.GetString(reader.GetOrdinal("Ad_Soyad")),
                    Eposta = reader.GetString(reader.GetOrdinal("E-Posta")),
                    Sifre = reader.GetString(reader.GetOrdinal("Sifre")),
                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                    Dahili = reader.GetString(reader.GetOrdinal("Dahili"))
                };

                dvDestekPersonelList.Add(dvDestekPersonel);
            }

            return dvDestekPersonelList;

        }

        public DvDestekPersonel getDvDestekPersonelById(int id)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetDvDestekPersonelByIdQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var dvDestekPersonel = new DvDestekPersonel
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    AdSoyad = reader.GetString(reader.GetOrdinal("Ad_Soyad")),
                    Eposta = reader.GetString(reader.GetOrdinal("E-Posta")),
                    Sifre = reader.GetString(reader.GetOrdinal("Sifre")),
                    Telefon = reader.GetString(reader.GetOrdinal("Telefon")),
                    Dahili = reader.GetString(reader.GetOrdinal("Dahili"))
                };

                return dvDestekPersonel;
            }

            return null;

        }


        // DvDestekUrunler

        public void insertDvDestekUrunler(DvDestekUrunler dvDestekUrunler)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(InsertDvDestekUrunlerQuery, connection);
            command.Parameters.AddWithValue("@Urun", dvDestekUrunler.Urun);
            command.ExecuteNonQuery();


        }

        public void updateDvDestekUrunler(DvDestekUrunler dvDestekUrunler)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(UpdateDvDestekUrunlerQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekUrunler.Id);
            command.Parameters.AddWithValue("@Urun", dvDestekUrunler.Urun);
            command.ExecuteNonQuery();

        }

        public void deleteDvDestekUrunler(DvDestekUrunler dvDestekUrunler)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(DeleteDvDestekUrunlerQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekUrunler.Id);
            command.ExecuteNonQuery();
        }

        public List<DvDestekUrunler> getAllDvDestekUrunler()
        {
            var dvDestekUrunler = new List<DvDestekUrunler>();

            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetAllDvDestekUrunlerQuery, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekUrunler1 = new DvDestekUrunler
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Urun = reader.GetString(reader.GetOrdinal("Urun"))
                };
                dvDestekUrunler.Add(dvDestekUrunler1);
            }

            return dvDestekUrunler;
        }

        public List<DvDestekUrunler> searchDvDestekUrunler(string search)
        {
            var dvDestekUrunlerList = new List<DvDestekUrunler>();
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetSearchDvDestekUrunlerQuery, connection);

            command.Parameters.AddWithValue("@Search", "%" + search + "%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekUrunler = new DvDestekUrunler
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Urun = reader.GetString(reader.GetOrdinal("Urun"))
                };

                dvDestekUrunlerList.Add(dvDestekUrunler);
            }

            return dvDestekUrunlerList;

        }

        public DvDestekUrunler getDvDestekUrunlerById(int id)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetDvDestekUrunlerByIdQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var dvDestekUrunler = new DvDestekUrunler
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Urun = reader.GetString(reader.GetOrdinal("Urun"))
                };

                return dvDestekUrunler;
            }

            return null;
        }



        //DvDestekUzak
        public void insertDvDestekUzak(DvDestekUzak dvDestekUzak)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(InsertDvDestekUzakQuery, connection);
            command.Parameters.AddWithValue("@Baglanan", dvDestekUzak.Baglanan);
            command.Parameters.AddWithValue("@BaglananUniq", dvDestekUzak.BaglananUniq);
            command.Parameters.AddWithValue("@BaglananIp", dvDestekUzak.BaglananIp); // Corrected typo
            command.Parameters.AddWithValue("@Yon", dvDestekUzak.Yon);
            command.Parameters.AddWithValue("@Musteri", dvDestekUzak.Musteri);
            command.Parameters.AddWithValue("@MusteriUniq", dvDestekUzak.MusteriUniq);
            command.Parameters.AddWithValue("@MusteriIp", dvDestekUzak.MusteriIp);
            command.Parameters.AddWithValue("@BaglantiSaat", dvDestekUzak.BaglantiSaat);
            command.Parameters.AddWithValue("@BaglantiTarih", dvDestekUzak.BaglantiTarih);
            command.Parameters.AddWithValue("@BaglantiSure", dvDestekUzak.BaglantiSure);
            command.Parameters.AddWithValue("@BaglantiAciklama", dvDestekUzak.BaglantiAciklama);
            command.Parameters.AddWithValue("@BaglantiDestekNo", dvDestekUzak.BaglantiDestekNo);
            command.Parameters.AddWithValue("@BaglantiUniq", dvDestekUzak.BaglantiUniq);
            command.Parameters.AddWithValue("@MusteriErpId", dvDestekUzak.MusteriErpId); // Corrected case
            command.Parameters.AddWithValue("@DestekUrunId", dvDestekUzak.DestekUrunId);
            command.Parameters.AddWithValue("@TalepDetay", dvDestekUzak.TalepDetay);
            command.Parameters.AddWithValue("@ToplamSure", dvDestekUzak.ToplamSure);
            command.ExecuteNonQuery();
        }

        public void updateDvDestekUzak(DvDestekUzak dvDestekUzak)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(UpdateDvDestekUzakQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekUzak.ID);
            command.Parameters.AddWithValue("@Baglanan", dvDestekUzak.Baglanan);
            command.Parameters.AddWithValue("@BaglananUniq", dvDestekUzak.BaglananUniq);
            command.Parameters.AddWithValue("@BaglananIp", dvDestekUzak.BaglananIp); // Corrected typo
            command.Parameters.AddWithValue("@Yon", dvDestekUzak.Yon);
            command.Parameters.AddWithValue("@Musteri", dvDestekUzak.Musteri);
            command.Parameters.AddWithValue("@MusteriUniq", dvDestekUzak.MusteriUniq);
            command.Parameters.AddWithValue("@MusteriIp", dvDestekUzak.MusteriIp);
            command.Parameters.AddWithValue("@BaglantiSaat", dvDestekUzak.BaglantiSaat);
            command.Parameters.AddWithValue("@BaglantiTarih", dvDestekUzak.BaglantiTarih);
            command.Parameters.AddWithValue("@BaglantiSure", dvDestekUzak.BaglantiSure);
            command.Parameters.AddWithValue("@BaglantiAciklama", dvDestekUzak.BaglantiAciklama);
            command.Parameters.AddWithValue("@BaglantiDestekNo", dvDestekUzak.BaglantiDestekNo);
            command.Parameters.AddWithValue("@BaglantiUniq", dvDestekUzak.BaglantiUniq);
            command.Parameters.AddWithValue("@MusteriErpId", dvDestekUzak.MusteriErpId); // Corrected case
            command.Parameters.AddWithValue("@DestekUrunId", dvDestekUzak.DestekUrunId);
            command.Parameters.AddWithValue("@TalepDetay", dvDestekUzak.TalepDetay);
            command.Parameters.AddWithValue("@ToplamSure", dvDestekUzak.ToplamSure);
            command.ExecuteNonQuery();

        }

        public void deleteDvDestekUzak(DvDestekUzak dvDestekUzak)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(DeleteDvDestekUzakQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekUzak.ID);
            command.ExecuteNonQuery();
        }

        public List<DvDestekUzak> getAllDvDestekUzak()
        {
            var dvDestekUzak = new List<DvDestekUzak>();

            using var connection = DbConnector.GetConnection();
            using var command = new SqlCommand(GetAllDvDestekUzakQuery, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekUzak1 = new DvDestekUzak
                {
                    ID = reader.GetInt32(reader.GetOrdinal("id")),
                    Baglanan = reader.GetString(reader.GetOrdinal("Baglanan")),
                    BaglananUniq = reader.GetString(reader.GetOrdinal("BaglananUniq")),
                    BaglananIp = reader.GetString(reader.GetOrdinal("BaglananIp")),
                    Yon = reader.GetString(reader.GetOrdinal("Yon")),
                    Musteri = reader.GetString(reader.GetOrdinal("Musteri")),
                    MusteriUniq = reader.GetString(reader.GetOrdinal("MusteriUniq")),
                    MusteriIp = reader.GetString(reader.GetOrdinal("MusteriIp")),
                    BaglantiSaat = reader.GetTimeSpan(reader.GetOrdinal("BaglantiSaat")),
                    BaglantiTarih = reader.GetDateTime(reader.GetOrdinal("BaglantiTarih")),
                    BaglantiSure = reader.GetTimeSpan(reader.GetOrdinal("BaglantiSure")),
                    BaglantiAciklama = reader.GetString(reader.GetOrdinal("BaglantiAciklama")),
                    BaglantiDestekNo = reader.GetString(reader.GetOrdinal("BaglantiDestekNo")),
                    BaglantiUniq = reader.GetString(reader.GetOrdinal("BaglantiUniq")),
                    MusteriErpId = reader.GetInt32(reader.GetOrdinal("MusteriErpId")), // Corrected to GetInt32
                    DestekUrunId = reader.GetInt32(reader.GetOrdinal("DestekUrunId")),
                    TalepDetay = reader.GetString(reader.GetOrdinal("TalepDetay")),
                    ToplamSure = reader.GetTimeSpan(reader.GetOrdinal("ToplamSure"))
                };
                dvDestekUzak.Add(dvDestekUzak1);
            }

            return dvDestekUzak;
        }


        public List<DvDestekUzak> searchDvDestekUzak(string search)
        {
            var dvDestekUzakList = new List<DvDestekUzak>();
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetSearchDvDestekUzakQuery, connection);

            command.Parameters.AddWithValue("@Search", "%" + search + "%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekUzak = new DvDestekUzak
                {
                    ID = reader.GetInt32(reader.GetOrdinal("id")),
                    Baglanan = reader.GetString(reader.GetOrdinal("Baglanan")),
                    BaglananUniq = reader.GetString(reader.GetOrdinal("BaglananUniq")),
                    BaglananIp = reader.GetString(reader.GetOrdinal("BaglananIp")), // Corrected typo
                    Yon = reader.GetInt32(reader.GetOrdinal("Yon")).ToString(),
                    Musteri = reader.GetString(reader.GetOrdinal("Musteri")),
                    MusteriUniq = reader.GetString(reader.GetOrdinal("MusteriUniq")),
                    MusteriIp = reader.GetString(reader.GetOrdinal("MusteriIp")),
                    BaglantiSaat = reader.GetTimeSpan(reader.GetOrdinal("BaglantiSaat")),
                    BaglantiTarih = reader.GetDateTime(reader.GetOrdinal("BaglantiTarih")),
                    BaglantiSure = reader.GetTimeSpan(reader.GetOrdinal("BaglantiSure")),
                    BaglantiAciklama = reader.GetString(reader.GetOrdinal("BaglantiAciklama")),
                    BaglantiDestekNo = reader.GetString(reader.GetOrdinal("BaglantiDestekNo")),
                    BaglantiUniq = reader.GetString(reader.GetOrdinal("BaglantiUniq")),
                    MusteriErpId = Convert.ToInt32(reader.GetString(reader.GetOrdinal("MusteriErpId"))), // Corrected case
                    DestekUrunId = reader.GetInt32(reader.GetOrdinal("DestekUrunId")),
                    TalepDetay = reader.GetString(reader.GetOrdinal("TalepDetay")),
                    ToplamSure = reader.GetTimeSpan(reader.GetOrdinal("ToplamSure"))
                };

                dvDestekUzakList.Add(dvDestekUzak);
            }

            return dvDestekUzakList;

        }

        public DvDestekUzak getDvDestekUzakById(int id)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetDvDestekUzakByIdQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var dvDestekUzak = new DvDestekUzak
                {
                    ID = reader.GetInt32(reader.GetOrdinal("id")),
                    Baglanan = reader.GetString(reader.GetOrdinal("Baglanan")),
                    BaglananUniq = reader.GetString(reader.GetOrdinal("BaglananUniq")),
                    BaglananIp = reader.GetString(reader.GetOrdinal("BaglananIp")), // Corrected typo
                    Yon = reader.GetString(reader.GetOrdinal("Yon")),
                    Musteri = reader.GetString(reader.GetOrdinal("Musteri")),
                    MusteriUniq = reader.GetString(reader.GetOrdinal("MusteriUniq")),
                    MusteriIp = reader.GetString(reader.GetOrdinal("MusteriIp")),
                    BaglantiSaat = reader.GetTimeSpan(reader.GetOrdinal("BaglantiSaat")),
                    BaglantiTarih = reader.GetDateTime(reader.GetOrdinal("BaglantiTarih")),
                    BaglantiSure = reader.GetTimeSpan(reader.GetOrdinal("BaglantiSure")),
                    BaglantiAciklama = reader.GetString(reader.GetOrdinal("BaglantiAciklama")),
                    BaglantiDestekNo = reader.GetString(reader.GetOrdinal("BaglantiDestekNo")),
                    BaglantiUniq = reader.GetString(reader.GetOrdinal("BaglantiUniq")),
                    MusteriErpId = Convert.ToInt32(reader.GetString(reader.GetOrdinal("MusteriErpId"))), // Corrected case
                    DestekUrunId = reader.GetInt32(reader.GetOrdinal("DestekUrunId")),
                    TalepDetay = reader.GetString(reader.GetOrdinal("TalepDetay")),
                    ToplamSure = reader.GetTimeSpan(reader.GetOrdinal("ToplamSure"))
                };

                return dvDestekUzak;
            }

            return null;

        }


        //DvDestekUzakDetay
        public void insertDvDestekUzakDetay(DvDestekUzakDetay dvDestekUzakDetay)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(InsertDvDestekUzakDetayQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekUzakDetay.Id);
            command.Parameters.AddWithValue("@DestekId", dvDestekUzakDetay.DestekId);
            command.Parameters.AddWithValue("@PersonelId", dvDestekUzakDetay.PersonelId);
            command.Parameters.AddWithValue("@YapilanIslem", dvDestekUzakDetay.YapilanIslem);
            command.Parameters.AddWithValue("@IslemTarihi", dvDestekUzakDetay.IslemTarihi);
            command.Parameters.AddWithValue("@DestekVerilenKisi", dvDestekUzakDetay.DestekVerilenKisi);
            command.ExecuteNonQuery();

        }

        public void updateDvDestekUzakDetay(DvDestekUzakDetay dvDestekUzakDetay)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(UpdateDvDestekUzakDetayQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekUzakDetay.Id);
            command.Parameters.AddWithValue("@DestekId", dvDestekUzakDetay.DestekId);
            command.Parameters.AddWithValue("@PersonelId", dvDestekUzakDetay.PersonelId);
            command.Parameters.AddWithValue("@YapilanIslem", dvDestekUzakDetay.YapilanIslem);
            command.Parameters.AddWithValue("@IslemTarihi", dvDestekUzakDetay.IslemTarihi);
            command.Parameters.AddWithValue("@DestekVerilenKisi", dvDestekUzakDetay.DestekVerilenKisi);
            command.ExecuteNonQuery();
        }

        public void deleteDvDestekUzakDetay(DvDestekUzakDetay dvDestekUzakDetay)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(DeleteDvDestekUzakDetayQuery, connection);
            command.Parameters.AddWithValue("@Id", dvDestekUzakDetay.Id);
            command.ExecuteNonQuery();
        }

        public List<DvDestekUzakDetay> getAllDvDestekUzakDetay()
        {
            var dvDestekUzakDetay = new List<DvDestekUzakDetay>();

            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetAllDvDestekUzakDetayQuery, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekUzakDetay1 = new DvDestekUzakDetay
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    DestekId = reader.GetInt32(reader.GetOrdinal("DestekId")),
                    PersonelId = reader.GetInt32(reader.GetOrdinal("PersonelId")),
                    YapilanIslem = reader.GetString(reader.GetOrdinal("YapilanIslem")),
                    IslemTarihi = reader.GetDateTime(reader.GetOrdinal("IslemTarihi")),
                    DestekVerilenKisi = Convert.ToInt32(reader.GetString(reader.GetOrdinal("DestekVerilenKisi")))
                };
                dvDestekUzakDetay.Add(dvDestekUzakDetay1);
            }

            return dvDestekUzakDetay;

        }

        public List<DvDestekUzakDetay> searchDvDestekUzakDetay(string search)
        {
            var dvDestekUzakDetayList = new List<DvDestekUzakDetay>();
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetSearchDvDestekUzakDetayQuery, connection);

            command.Parameters.AddWithValue("@Search", "%" + search + "%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dvDestekUzakDetay = new DvDestekUzakDetay
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    DestekId = reader.GetInt32(reader.GetOrdinal("DestekId")),
                    PersonelId = reader.GetInt32(reader.GetOrdinal("PersonelId")),
                    YapilanIslem = reader.GetString(reader.GetOrdinal("YapilanIslem")),
                    IslemTarihi = reader.GetDateTime(reader.GetOrdinal("IslemTarihi")),
                    DestekVerilenKisi = Convert.ToInt32(reader.GetString(reader.GetOrdinal("DestekVerilenKisi")))
                };

                dvDestekUzakDetayList.Add(dvDestekUzakDetay);
            }

            return dvDestekUzakDetayList;

        }

        public DvDestekUzakDetay getDvDestekUzakDetayById(int id)
        {
            using var connection = DbConnector.GetConnection();

            using var command = new SqlCommand(GetDvDestekUzakDetayByIdQuery, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var dvDestekUzakDetay = new DvDestekUzakDetay
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    DestekId = reader.GetInt32(reader.GetOrdinal("DestekId")),
                    PersonelId = reader.GetInt32(reader.GetOrdinal("PersonelId")),
                    YapilanIslem = reader.GetString(reader.GetOrdinal("YapilanIslem")),
                    IslemTarihi = reader.GetDateTime(reader.GetOrdinal("IslemTarihi")),
                    DestekVerilenKisi = Convert.ToInt32(reader.GetString(reader.GetOrdinal("DestekVerilenKisi")))
                };

                return dvDestekUzakDetay;
            }

            return null;

        }

        public async Task<string> ConvertQueryResultToXmlAsync(string query, string fileName)
        {
            // Get the full path asynchronously
            string filePath = await GetSaveFilePathAsync(fileName);

            using (var connection = DbConnector.GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteXmlReader())
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            while (reader.Read())
                            {
                                writer.Write(reader.ReadOuterXml());
                            }
                        }
                    }
                }
            }

            return filePath;
        }
        //GetSaveFilePathAsync for save file to dowload
        public static Task<string> GetSaveFilePathAsync(string fileName)
        {
            // Get folder path
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Downloads");

            // Create folder if not exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Get file path
            string filePath = Path.Combine(folderPath, fileName);

            return Task.FromResult(filePath);
        }



        //<kslr><Id>25</Id><Ad_Soyad>AdSoyad24</Ad_Soyad><Gorev>Gorev24</Gorev><Mail>Mail24</Mail><Telefon>Telefon24</Telefon><Durum>24</Durum><BagliFirmaId>25</BagliFirmaId><Aciklama>Aciklama24</Aciklama></kslr>
        //xml to table

        public void InsertXmlDataToDatabase(string xmlFilePath, string tableName)
        {
            // Read XML file
            DataSet dataSet = new DataSet();
            using (StreamReader reader = new StreamReader(xmlFilePath))
            {
                dataSet.ReadXml(reader);
            }

            // Get DataTable
            DataTable dataTable = dataSet.Tables[0];

            // Insert data to database
            using (var connection = DbConnector.GetConnection())
            {
                // Begin transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert each row
                        foreach (DataRow row in dataTable.Rows)
                        {
                            // Generate SQL command
                            using (var command = connection.CreateCommand())
                            {
                                // Set transaction
                                command.Transaction = transaction;
                                command.CommandText = GenerateInsertCommand(dataTable, tableName);

                                foreach (DataColumn column in dataTable.Columns)
                                {
                                    // Add parameters except Id
                                    if (!column.ColumnName.Equals("Id", StringComparison.OrdinalIgnoreCase))
                                    {
                                        // Add parameter
                                        command.Parameters.AddWithValue("@" + column.ColumnName, row[column]);
                                    }
                                }
                                // Execute command
                                command.ExecuteNonQuery();
                            }
                        }
                        // Commit transaction
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Rollback transaction
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private string GenerateInsertCommand(DataTable dataTable, string tableName)
        {
            // Get all columns except Id
            var allColumns = dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
            var columnsToInsert = allColumns.Where(c => !c.Equals("Id", StringComparison.OrdinalIgnoreCase));

            // Generate SQL command
            string columnNames = string.Join(", ", columnsToInsert);
            // Generate parameter names
            string parameterNames = string.Join(", ", columnsToInsert.Select(c => "@" + c));

            // INSERT INTO TableName and column names and values
            return $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";
        }


    }
}