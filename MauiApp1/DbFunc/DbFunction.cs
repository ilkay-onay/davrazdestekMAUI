using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


public class DbFunction : IDbFunctions
{
    //DvDestekCagrilar
    private const string InsertDvDestekCagrilarQuery = @"INSERT INTO Dv_Destek_Cagrilar (Tarih, Arayan, Aranan, ToplamSure, BeklemeSuresi, GorusmeSuresi, Sonuc, Tipi) 
                      VALUES (@Tarih, @Arayan, @Aranan, @ToplamSure, @BeklemeSuresi, @GorusmeSuresi, @Sonuc, @Tipi)";
    private const string UpdateDvDestekCagrilarQuery = @"UPDATE Dv_Destek_Cagrilar 
                      SET Tarih = @Tarih, Arayan = @Arayan, Aranan = @Aranan, ToplamSure = @ToplamSure, BeklemeSuresi = @BeklemeSuresi, GorusmeSuresi = @GorusmeSuresi, Sonuc = @Sonuc, Tipi = @Tipi
                      WHERE Id = @Id";
    private const string DaleteDvDestekCagrilarQuery = "DELETE FROM Dv_Destek_Cagrilar WHERE Id = @Id";
    private const string GetAllDvDestekKisilerQuery = "SELECT * FROM Dv_Destek_Kisiler";
    private const string SearchDvDestekCagrilarQuery = "SELECT * FROM Dv_Destek_Cagrilar WHERE Arayan LIKE @Search OR Aranan LIKE @Search";
    private const string GetByDestekCagrilarIdQuery = "SELECT * FROM Dv_Destek_Cagrilar WHERE Id = @Id";
    
    //DvDestekKisiler
    private const string InsertDvDestekKisilerQuery = @"INSERT INTO Dv_Destek_Kisiler (Ad_Soyad, Gorev, Mail, Telefon, Durum, BagliFirmaId, Aciklama) VALUES (@Ad_Soyad, @Gorev, @Mail, @Telefon, @Durum, @BagliFirmaId, @Aciklama)";
    private const string GetSearchDvDestekKisilerQuery ="SELECT * FROM Dv_Destek_Kisiler WHERE Ad_Soyad LIKE @Search OR Gorev LIKE @Search OR Mail LIKE @Search OR Telefon LIKE @Search OR Aciklama LIKE @Search";


    //DvDestekUrunler
    private const string InsertDvDestekUrunlerQuery = @"INSERT INTO Dv_Destek_Urunler (Urun) VALUES (@Urun)";
    
    
    //DvDestekMusteriUrun
    private const string InsertDvDestekMusteriUrunQuery = @"INSERT INTO Dv_Destek_Musteri_Urun (Id,MusteriId, UrunId,BaslamaTarihi,SonKullanimTarihi,Aciklama,Miktar,Durum,BirimId) VALUES (@Id,@MusteriId, @UrunId, @BaslamaTarihi, @SonKullanimTarihi, @Aciklama, @Miktar, @Durum, @BirimId)";

    
    //DvDestekMusteriUrunDetay
    private const string InsertDvDestekMusteriUrunDetayQuery = @"INSERT INTO Dv_Destek_Musteri_Urun_Detay (BaglantiId,Tarih,Aciklama,PersonelId) VALUES (@BaglantiId,@Tarih, @Aciklama, @PersonelId)";
    
    //DvDestekUzak
    private const string InsertDvDestekUzakQuery = @"INSERT INTO Dv_Destek_Uzak (Baglanan, BaglananUniq, BaglananIp, Yon, Musteri, MusteriUniq, MusteriIp, BaglantiSaat, BaglantiTarih, BaglantiSure, BaglantiAciklama, BaglantiDestekNo, BaglantiUniq, MusteriErpId, DestekUrunId, TalepDetay, ToplamSure) VALUES (@Baglanan, @BaglananUniq, @BaglananIp, @Yon, @Musteri, @MusteriUniq, @MusteriIp, @BaglantiSaat, @BaglantiTarih, @BaglantiSure, @BaglantiAciklama, @BaglantiDestekNo, @BaglantiUniq, @MusteriErpId, @DestekUrunId, @TalepDetay, @ToplamSure)";    
    
    
    //DvDestekUzakDetay
    private const string InsertDvDestekUzakDetayQuery =@"INSERT INTO Dv_Destek_Uzak_Detay(Id,DestekId,PersonelId,YapilanIslem,IslemTarihi,DestekVerilenKisi) VALUES (@Id,@DestekId, @PersonelId, @YapilanIslem, @IslemTarihi, @DestekVerilenKisi)";
    
    
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
        var query = "SELECT * FROM Dv_Destek_Cagrilar";

        using var command = new SqlCommand(query, connection);
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
        throw new NotImplementedException();
    }

    public void deleteDvDestekKisiler(DvDestekKisiler dvDestekKisiler)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void deleteDvDestekUrunler(DvDestekUrunler dvDestekUrunler)
    {
        throw new NotImplementedException();
    }

    public List<DvDestekUrunler> getAllDvDestekUrunler()
    {
        throw new NotImplementedException();
    }

    public List<DvDestekUrunler> searchDvDestekUrunler(string search)
    {
        throw new NotImplementedException();
    }

    public DvDestekUrunler getDvDestekUrunlerById(int id)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void deleteDvDestekMusteriUrun(DvDestekMusteriUrun dvDestekMusteriUrun)
    {
        throw new NotImplementedException();
    }

    public List<DvDestekMusteriUrun> getAllDvDestekMusteriUrun()
    {
        throw new NotImplementedException();
    }

    public List<DvDestekMusteriUrun> searchDvDestekMusteriUrun(string search)
    {
        throw new NotImplementedException();
    }

    public DvDestekMusteriUrun getDvDestekMusteriUrunById(int id)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void deleteDvDestekMusteriUrunDetay(DvDestekMusteriUrunDetay dvDestekMusteriUrunDetay)
    {
        throw new NotImplementedException();
    }

    public List<DvDestekMusteriUrunDetay> getAllDvDestekMusteriUrunDetay()
    {
        throw new NotImplementedException();
    }

    public List<DvDestekMusteriUrunDetay> searchDvDestekMusteriUrunDetay(string search)
    {
        throw new NotImplementedException();
    }

    public DvDestekMusteriUrunDetay getDvDestekMusteriUrunDetayById(int id)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void deleteDvDestekUzak(DvDestekUzak dvDestekUzak)
    {
        throw new NotImplementedException();
    }

    public List<DvDestekUzak> getAllDvDestekUzak()
    {
        throw new NotImplementedException();
    }

    public List<DvDestekUzak> searchDvDestekUzak(string search)
    {
        throw new NotImplementedException();
    }

    public DvDestekUzak getDvDestekUzakById(int id)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void deleteDvDestekUzakDetay(DvDestekUzakDetay dvDestekUzakDetay)
    {
        throw new NotImplementedException();
    }

    public List<DvDestekUzakDetay> getAllDvDestekUzakDetay()
    {
        throw new NotImplementedException();
    }

    public List<DvDestekUzakDetay> searchDvDestekUzakDetay(string search)
    {
        throw new NotImplementedException();
    }

    public DvDestekUzakDetay getDvDestekUzakDetayById(int id)
    {
        throw new NotImplementedException();
    }
}