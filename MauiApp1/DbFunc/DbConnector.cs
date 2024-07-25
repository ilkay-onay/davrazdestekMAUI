using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

public static class DbConnector
{
	private static readonly string ConnectionString = "Server=192.168.100.220;Database=Dv;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;";

	public static SqlConnection GetConnection()
	{
		try
		{
			var connection = new SqlConnection(ConnectionString);
			connection.Open();
			return connection;
		}
		catch (Exception e)
		{
			throw new Exception("Error connecting to the database: " + e.Message);
		}
	}
}