using MauiApp1.Services;
using MauiApp1.Models;
using System.Collections.ObjectModel;
using ClosedXML.Excel;
using System.Linq;
using Microsoft.Extensions.Logging;

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
            BindingContext = this; // Bind the page's BindingContext to itself
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
                // Logger'ı ve DatabaseService'i alın
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<EditRemoteConnectionPage>();

                await Navigation.PushAsync(new EditRemoteConnectionPage(remoteConnection, _databaseService, logger));
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