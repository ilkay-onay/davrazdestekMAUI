using MauiApp1.Services;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using System.Globalization;
using Dapper;

namespace MauiApp1
{
    public partial class RemoteConnectionsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private int _currentPage = 1;
        private const int PageSize = 10;
        private int _totalPageCount;
        private bool _isAscending = true; // Add a field to track the current sort order

        public ObservableCollection<RemoteConnection> RemoteConnections { get; set; }

        public RemoteConnectionsPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=MAUI;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            RemoteConnections = new ObservableCollection<RemoteConnection>();
            BindingContext = this;
            LoadRemoteConnectionsAsync();
        }

        public RemoteConnectionsPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            RemoteConnections = new ObservableCollection<RemoteConnection>();
            BindingContext = this;
            LoadRemoteConnectionsAsync();
        }

        private async void OnPreviousClicked(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadRemoteConnectionsAsync();
            }
        }

        private async void OnNextClicked(object sender, EventArgs e)
        {
            if (_currentPage < _totalPageCount)
            {
                _currentPage++;
                await LoadRemoteConnectionsAsync();
            }
        }

        private async void OnSortClicked(object sender, EventArgs e)
        {
            _isAscending = !_isAscending; // Toggle the sort order
            await LoadRemoteConnectionsAsync();
        }

        private async Task LoadRemoteConnectionsAsync()
        {
            try
            {
                var remoteConnections = await _databaseService.GetRemoteConnectionsAsync(_currentPage, PageSize, _isAscending);
                var totalRemoteConnections = await _databaseService.GetTotalRemoteConnectionCountAsync();
                _totalPageCount = (int)Math.Ceiling(totalRemoteConnections / (double)PageSize);

                RemoteConnections.Clear();
                foreach (var remoteConnection in remoteConnections)
                {
                    RemoteConnections.Add(remoteConnection);
                }

                PageInfoLabel.Text = $"Page {_currentPage} of {_totalPageCount}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadRemoteConnectionsAsync: {ex.Message}");
                await DisplayAlert("Error", "Failed to load remote connections. Please try again later.", "OK");
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var remoteConnection = button?.CommandParameter as RemoteConnection;

            if (remoteConnection != null)
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<EditRemoteConnectionPage>();

                await Navigation.PushAsync(new EditRemoteConnectionPage(remoteConnection, _databaseService, logger));
            }
        }

        private async void OnImportFromExcelClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an Excel file"
                });

                if (result != null)
                {
                    using (var stream = await result.OpenReadAsync())
                    {
                        using (var workbook = new XLWorkbook(stream))
                        {
                            var worksheet = workbook.Worksheet(1);
                            var rows = worksheet.RowsUsed().Skip(1); // Skip header row

                            var remoteConnections = new List<RemoteConnection>();

                            foreach (var row in rows)
                            {
                                var remoteConnection = new RemoteConnection
                                {
                                    Baglanan = row.Cell(1).GetString(),
                                    BaglananUniq = row.Cell(2).GetString(),
                                    BaglananIp = row.Cell(3).GetString(),
                                    Yon = row.Cell(4).GetString(),
                                    Musteri = row.Cell(5).GetString(),
                                    MusteriUniq = row.Cell(6).GetString(),
                                    MusteriIp = row.Cell(7).GetString(),
                                    BaglantiSaat = TimeSpan.Parse(row.Cell(8).GetString(), CultureInfo.InvariantCulture),
                                    BaglantiTarih = DateTime.Parse(row.Cell(9).GetString(), CultureInfo.InvariantCulture),
                                    BaglantiSure = TimeSpan.Parse(row.Cell(10).GetString()), // Corrected parsing for TimeSpan
                                    BaglantiAciklama = row.Cell(11).GetString(),
                                    BaglantiDestekNo = row.Cell(12).GetString(),
                                    BaglantiUniq = row.Cell(13).GetString()
                                };
                                remoteConnections.Add(remoteConnection);
                            }

                            // Insert the data into the database
                            await InsertRemoteConnectionsAsync(remoteConnections);
                        }
                    }

                    await DisplayAlert("Success", "Data imported successfully!", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnImportFromExcelClicked: {ex.Message}");
                await DisplayAlert("Error", "Failed to import data. Please try again later.", "OK");
            }
        }

        private async Task InsertRemoteConnectionsAsync(List<RemoteConnection> remoteConnections)
        {
            try
            {
                using (var connection = new SqlConnection("Server=192.168.100.220;Database=MAUI;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;"))
                {
                    await connection.OpenAsync();
                    var maxId = await connection.ExecuteScalarAsync<int>("SELECT MAX(ID) FROM dbo.Dv_Destek_Uzak");
                    int newId = maxId + 1;

                    foreach (var remoteConnection in remoteConnections)
                    {
                        remoteConnection.ID = newId++;

                        string query = @"
                            INSERT INTO dbo.Dv_Destek_Uzak (Baglanan, BaglananUniq, BaglananIp, Yon, Musteri, MusteriUniq, MusteriIp, BaglantiSaat, BaglantiTarih, BaglantiSure, BaglantiAciklama, BaglantiDestekNo, BaglantiUniq)
                            VALUES (@Baglanan, @BaglananUniq, @BaglananIp, @Yon, @Musteri, @MusteriUniq, @MusteriIp, @BaglantiSaat, @BaglantiTarih, @BaglantiSure, @BaglantiAciklama, @BaglantiDestekNo, @BaglantiUniq)";

                        await connection.ExecuteAsync(query, remoteConnection);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in InsertRemoteConnectionsAsync: {ex.Message}");
                throw;
            }
        }

        private async void OnExportToExcelClicked(object sender, EventArgs e)
        {
            try
            {
                var allRemoteConnections = await _databaseService.GetAllRemoteConnectionsAsync(); // Fetch all data
                string filePath = await GetSaveFilePathAsync();

                if (string.IsNullOrEmpty(filePath))
                    return;

                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Remote Connections");

                    // Add headers
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Baglanan";
                    worksheet.Cell(1, 3).Value = "BaglananUniq";
                    worksheet.Cell(1, 4).Value = "BaglananIp";
                    worksheet.Cell(1, 5).Value = "Yon";
                    worksheet.Cell(1, 6).Value = "Musteri";
                    worksheet.Cell(1, 7).Value = "MusteriUniq";
                    worksheet.Cell(1, 8).Value = "MusteriIp";
                    worksheet.Cell(1, 9).Value = "BaglantiSaat";
                    worksheet.Cell(1, 10).Value = "BaglantiTarih";
                    worksheet.Cell(1, 11).Value = "BaglantiSure";
                    worksheet.Cell(1, 12).Value = "BaglantiAciklama";
                    worksheet.Cell(1, 13).Value = "BaglantiDestekNo";
                    worksheet.Cell(1, 14).Value = "BaglantiUniq";

                    int row = 2;
                    foreach (var rc in allRemoteConnections)
                    {
                        worksheet.Cell(row, 1).Value = rc.ID;
                        worksheet.Cell(row, 2).Value = rc.Baglanan;
                        worksheet.Cell(row, 3).Value = rc.BaglananUniq;
                        worksheet.Cell(row, 4).Value = rc.BaglananIp;
                        worksheet.Cell(row, 5).Value = rc.Yon;
                        worksheet.Cell(row, 6).Value = rc.Musteri;
                        worksheet.Cell(row, 7).Value = rc.MusteriUniq;
                        worksheet.Cell(row, 8).Value = rc.MusteriIp;
                        worksheet.Cell(row, 9).Value = rc.BaglantiSaat;
                        worksheet.Cell(row, 10).Value = rc.BaglantiTarih;
                        worksheet.Cell(row, 11).Value = rc.BaglantiSure;
                        worksheet.Cell(row, 12).Value = rc.BaglantiAciklama;
                        worksheet.Cell(row, 13).Value = rc.BaglantiDestekNo;
                        worksheet.Cell(row, 14).Value = rc.BaglantiUniq;
                        row++;
                    }

                    workbook.SaveAs(filePath);
                }

                await DisplayAlert("Success", $"Data exported successfully to {filePath}!", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnExportToExcelClicked: {ex.Message}");
                await DisplayAlert("Error", "Failed to export data. Please try again later.", "OK");
            }
        }

        private async Task<string> GetSaveFilePathAsync()
        {
            try
            {
                string saveDirectory = string.Empty;

                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    saveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                }
                else
                {
                    saveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }

                string fileName = $"RemoteConnections_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                string filePath = Path.Combine(saveDirectory, fileName);

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSaveFilePathAsync: {ex.Message}");
                await DisplayAlert("Error", "Failed to get file path. Please try again later.", "OK");
            }

            return null;
        }
    }
}
