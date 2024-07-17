using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


public class DbFunction:IDbFunctions
{
    public void InsertDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar)
    {
        using var connection = DbConnector.GetConnection();
        var query = @"INSERT INTO Dv_Destek_Cagrilar (Tarih, Arayan, Aranan, ToplamSure, BeklemeSuresi, GorusmeSuresi, Sonuc, Tipi) 
                      VALUES (@Tarih, @Arayan, @Aranan, @ToplamSure, @BeklemeSuresi, @GorusmeSuresi, @Sonuc, @Tipi)";
        
        using var command = new SqlCommand(query, connection);
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
        var query = @"UPDATE Dv_Destek_Cagrilar 
                      SET Tarih = @Tarih, Arayan = @Arayan, Aranan = @Aranan, ToplamSure = @ToplamSure, BeklemeSuresi = @BeklemeSuresi, GorusmeSuresi = @GorusmeSuresi, Sonuc = @Sonuc, Tipi = @Tipi
                      WHERE Id = @Id";
        using var command = new SqlCommand(query, connection);
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
      public void DeleteDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar){
        using var connection = DbConnector.GetConnection();
        var query = "DELETE FROM Dv_Destek_Cagrilar WHERE Id = @Id";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", dvDestekCagrilar.Id);
        command.ExecuteNonQuery();
    }
    public List<DvDestekCagrilar> SearchDvDestekCagrilar(string search)
    {
        var dvDestekCagrilarList = new List<DvDestekCagrilar>();
        using var connection = DbConnector.GetConnection();
        var query = "SELECT * FROM Dv_Destek_Cagrilar WHERE Arayan LIKE @Search OR Aranan LIKE @Search";
        using var command = new SqlCommand(query, connection);
        
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
    public List<DvDestekCagrilar> GetByFirst100DvDestekCagrilar(){
        var dvDestekCagrilarList = new List<DvDestekCagrilar>();

        using var connection = DbConnector.GetConnection();
        var query = "SELECT TOP 100 * FROM Dv_Destek_Cagrilar";

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
    

}
