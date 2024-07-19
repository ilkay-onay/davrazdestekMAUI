using MauiApp1.Services;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using ClosedXML.Excel;
using Microsoft.Maui.Storage;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Dapper;

namespace MauiApp1
{
    public partial class RemoteConnectionsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private int _currentPage = 1;
        private const int PageSize = 10;
        private int _totalPageCount;

        public ObservableCollection<RemoteConnection> RemoteConnections { get; set; }

        public RemoteConnectionsPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=MAUI;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            RemoteConnections = new ObservableCollection<RemoteConnection>();
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

        private async Task LoadRemoteConnectionsAsync()
        {
            try
            {
                var remoteConnections = await _databaseService.GetRemoteConnectionsAsync(_currentPage, PageSize);
                var totalRemoteConnections = await _databaseService.GetTotalRemoteConnectionCountAsync();
                _totalPageCount = (int)Math.Ceiling(totalRemoteConnections / (double)PageSize);

                RemoteConnectionsGrid.Children.Clear();
                RemoteConnectionsGrid.RowDefinitions.Clear();

                // Add headers
                AddGridHeader("ID", 0);
                AddGridHeader("Baglanan", 1);
                AddGridHeader("BaglananUniq", 2);
                AddGridHeader("BaglananIp", 3);
                AddGridHeader("Yon", 4);
                AddGridHeader("Musteri", 5);
                AddGridHeader("MusteriUniq", 6);
                AddGridHeader("MusteriIp", 7);
                AddGridHeader("BaglantiSaat", 8);
                AddGridHeader("BaglantiTarih", 9);
                AddGridHeader("BaglantiSure", 10);
                AddGridHeader("BaglantiAciklama", 11);
                AddGridHeader("BaglantiDestekNo", 12);
                AddGridHeader("BaglantiUniq", 13);

                int row = 1;
                foreach (var remoteConnection in remoteConnections)
                {
                    AddGridRow();
                    AddGridCell(remoteConnection.ID.ToString(), row, 0);
                    AddGridCell(remoteConnection.Baglanan, row, 1);
                    AddGridCell(remoteConnection.BaglananUniq, row, 2);
                    AddGridCell(remoteConnection.BaglananIp, row, 3);
                    AddGridCell(remoteConnection.Yon, row, 4);
                    AddGridCell(remoteConnection.Musteri, row, 5);
                    AddGridCell(remoteConnection.MusteriUniq, row, 6);
                    AddGridCell(remoteConnection.MusteriIp, row, 7);
                    AddGridCell(remoteConnection.BaglantiSaat.ToString(), row, 8);
                    AddGridCell(remoteConnection.BaglantiTarih.ToString(), row, 9);
                    AddGridCell(remoteConnection.BaglantiSure.ToString(), row, 10);
                    AddGridCell(remoteConnection.BaglantiAciklama, row, 11);
                    AddGridCell(remoteConnection.BaglantiDestekNo, row, 12);
                    AddGridCell(remoteConnection.BaglantiUniq, row, 13);
                    row++;
                }

                PageInfoLabel.Text = $"Page {_currentPage} of {_totalPageCount}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadRemoteConnectionsAsync: {ex.Message}");
                await DisplayAlert("Error", "Failed to load remote connections. Please try again later.", "OK");
            }
        }

        private void AddGridHeader(string headerText, int column)
        {
            var frame = new Frame
            {
                Content = new Label
                {
                    Text = headerText,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(5)
                },
                BorderColor = Colors.Black,
                Margin = new Thickness(1)
            };
            RemoteConnectionsGrid.Children.Add(frame);
            Grid.SetRow(frame, 0);
            Grid.SetColumn(frame, column);
        }

        private void AddGridCell(string cellText, int row, int column)
        {
            var frame = new Frame
            {
                Content = new Label
                {
                    Text = cellText,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(5)
                },
                BorderColor = Colors.Black,
                Margin = new Thickness(1)
            };
            RemoteConnectionsGrid.Children.Add(frame);
            Grid.SetRow(frame, row);
            Grid.SetColumn(frame, column);
        }

        private void AddGridRow()
        {
            RemoteConnectionsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        private async void OnExportToExcelClicked(object sender, EventArgs e)
        {
            try
            {
                var allRemoteConnections = await _databaseService.GetAllRemoteConnectionsAsync(); // Fetch all data
                string filePath = await GetSaveFilePathAsync();

                if (string.IsNullOrEmpty(filePath))
                    return;

                using (var workbook = new XLWorkbook())
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

                await DisplayAlert("Success", "Data exported successfully!", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnExportToExcelClicked: {ex.Message}");
                await DisplayAlert("Error", "Failed to export data. Please try again later.", "OK");
            }
        }

        private async Task<string> GetSaveFilePathAsync()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select a location to save the Excel file"
            });

            if (result != null)
            {
                return result.FullPath;
            }

            return null;
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
    }
}
