
public class DbFunction : IDbFunctions
{
    //connection string= Server=192.168.100.220;Database=MAUI;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;
    public string connectionString = "Server=192.168.100.220;Database=MAUI;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;";

    public void InsertDvDestekCagrilar(DvDestekCagrilar dvDestekCagrilar)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var query = @"INSERT INTO Dv_Destek_Cagrilar (Tarih, Arayan, Aranan, ToplamSure, BeklemeSuresi, GorusmeSuresi, Sonuc, Tipi) 
                      VALUES (@Tarih, @Arayan, @Aranan, @ToplamSure, @BeklemeSuresi, @GorusmeSuresi, @Sonuc, @Tipi)";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Tarih", dvDestekCagrilar.Tarih);
                command.Parameters.AddWithValue("@Arayan", dvDestekCagrilar.Arayan);
                command.Parameters.AddWithValue("@Aranan", dvDestekCagrilar.Aranan);
                command.Parameters.AddWithValue("@ToplamSure", dvDestekCagrilar.ToplamSure);
                command.Parameters.AddWithValue("@BeklemeSuresi", dvDestekCagrilar.BeklemeSuresi);
                command.Parameters.AddWithValue("@GorusmeSuresi", dvDestekCagrilar.GorusmeSuresi);
                command.Parameters.AddWithValue("@Sonuc", dvDestekCagrilar.Sonuc);
                command.Parameters.AddWithValue("@Tipi", dvDestekCagrilar.Tipi);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
    public List<DvDestekCagrilar> getAllDvDestekCagrilar()
    {
        List<DvDestekCagrilar> dvDestekCagrilarList = new ArrayList<>();
        Connection conn = DbConnector.getConnection();
        Statement stmt = null;
        try
        {
            stmt = conn.createStatement();
            ResultSet rs = stmt.executeQuery(gelAllDvDestekCagrilar);
            while (rs.next())
            {
                DvDestekCagrilar dvDestekCagrilar = new DvDestekCagrilar();
                dvDestekCagrilar.setId(rs.getInt("id"));
                dvDestekCagrilar.setTarih(rs.getDate("tarih"));
                dvDestekCagrilar.setArayan(rs.getString("arayan"));
                dvDestekCagrilar.setAranan(rs.getString("aranan"));
                dvDestekCagrilar.setToplamSure(rs.getTime("toplamSure"));
                dvDestekCagrilar.setBeklemeSuresi(rs.getTime("beklemeSuresi"));
                dvDestekCagrilar.setGorusmeSuresi(rs.getTime("gorusmeSuresi"));
                dvDestekCagrilar.setSonuc(rs.getInt("sonuc"));
                dvDestekCagrilar.setTipi(rs.getShort("tipi"));
                dvDestekCagrilarList.add(dvDestekCagrilar);
            }
        }
        catch (SQLException e)
        {
            throw new RuntimeException(e);
        }
        finally
        {
            try
            {
                if (stmt != null) stmt.close();
                if (conn != null) conn.close();
            }
            catch (SQLException e)
            {
                e.printStackTrace();
            }
        }
        return dvDestekCagrilarList;
    }




}