using ClosedXML.Excel;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace MauiApp1
{
    public partial class CallsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private int _currentPage = 1;
        private const int PageSize = 5;
        private int _totalPageCount;
        private string _selectedAranan;

        public ObservableCollection<CallRecord> Calls { get; set; }
        public ObservableCollection<string> UniqueArananList { get; set; }
        public DateTime StartDate { get; set; } // Will be set in constructor
        public DateTime EndDate { get; set; } // Will be set in constructor

        public CallsPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=Dv;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            Calls = new ObservableCollection<CallRecord>();
            UniqueArananList = new ObservableCollection<string>();
            BindingContext = this;

            // Set default dates
            EndDate = DateTime.Today; // Default to today
            StartDate = EndDate.AddMonths(-1); // Default to one month before today

            StartDatePicker.Date = StartDate;
            EndDatePicker.Date = EndDate;

            CallsCollectionView.ItemsSource = Calls;
            LoadCallsAsync();
            LoadUniqueArananListAsync();
        }

        private async void OnPreviousClicked(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadCallsAsync();
            }
        }

        private async void OnNextClicked(object sender, EventArgs e)
        {
            if (_currentPage < _totalPageCount)
            {
                _currentPage++;
                await LoadCallsAsync();
            }
        }

        private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            await LoadCallsAsync(e.NewTextValue);
        }

        private async void OnFilterClicked(object sender, EventArgs e)
        {
            await LoadCallsAsync();
        }

        private async void OnArananPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ArananPicker.SelectedIndex != -1)
            {
                _selectedAranan = ArananPicker.SelectedItem.ToString();
            }
            await LoadCallsAsync();
        }

        private async void OnResetClicked(object sender, EventArgs e)
        {
            // Reset the filters
            StartDatePicker.Date = DateTime.Today.AddMonths(-1);
            EndDatePicker.Date = DateTime.Today;
            SearchBar.Text = string.Empty;
            ArananPicker.SelectedIndex = -1;
            _selectedAranan = null;

            // Reload calls with default filter values
            await LoadCallsAsync();
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

                string fileName = $"CallsPage_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                string filePath = Path.Combine(saveDirectory, fileName);

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSaveFilePathAsync: {ex.Message}");
                await DisplayAlert("Error", "Failed to get file path. Please try again later.", "OK");
                return null;
            }
        }

        private async void OnExportToExcelClicked(object sender, EventArgs e)
        {
            try
            {
                var allCalls = await _databaseService.GetAllCallsAsync();
                string filePath = await GetSaveFilePathAsync();

                if (filePath == null)
                    return;

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Calls");

                    // Add headers
                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 2).Value = "Tarih";
                    worksheet.Cell(1, 3).Value = "Arayan";
                    worksheet.Cell(1, 4).Value = "Aranan";
                    worksheet.Cell(1, 5).Value = "ToplamSure";
                    worksheet.Cell(1, 6).Value = "BeklemeSuresi";
                    worksheet.Cell(1, 7).Value = "GorusmeSuresi";
                    worksheet.Cell(1, 8).Value = "Sonuc";
                    worksheet.Cell(1, 9).Value = "Tipi";

                    // Add data rows
                    int row = 2;
                    foreach (var call in allCalls)
                    {
                        worksheet.Cell(row, 1).Value = call.Id;
                        worksheet.Cell(row, 2).Value = call.Tarih;
                        worksheet.Cell(row, 3).Value = call.Arayan;
                        worksheet.Cell(row, 4).Value = call.Aranan;
                        worksheet.Cell(row, 5).Value = call.ToplamSure;
                        worksheet.Cell(row, 6).Value = call.BeklemeSuresi;
                        worksheet.Cell(row, 7).Value = call.GorusmeSuresi;
                        worksheet.Cell(row, 8).Value = call.Sonuc;
                        worksheet.Cell(row, 9).Value = call.Tipi;
                        row++;
                    }

                    // Save to local file
                    workbook.SaveAs(filePath);

                    await DisplayAlert("Success", $"Data exported successfully to {filePath}", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnExportToExcelClicked: {ex.Message}");
                await DisplayAlert("Error", "Failed to export data. Please try again later.", "OK");
            }
        }



        private async Task LoadCallsAsync(string searchQuery = "")
        {
            try
            {
                var calls = await _databaseService.GetCallsAsync(_currentPage, PageSize, searchQuery, StartDate, EndDate, _selectedAranan);
                var totalCalls = await _databaseService.GetTotalCallCountAsync(StartDate, EndDate, _selectedAranan, searchQuery);
                _totalPageCount = (int)Math.Ceiling(totalCalls / (double)PageSize);

                Calls.Clear();
                foreach (var call in calls)
                {
                    Calls.Add(call);
                }

                PageInfoLabel.Text = $"Page {_currentPage} / {_totalPageCount}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading calls: {ex.Message}");
            }
        }


        private async Task LoadUniqueArananListAsync()
        {
            try
            {
                var uniqueAranan = await _databaseService.GetUniqueArananListAsync();
                UniqueArananList.Clear();
                foreach (var aranan in uniqueAranan)
                {
                    UniqueArananList.Add(aranan);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading unique Aranan list: {ex.Message}");
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

                            var callRecords = new List<CallRecord>();

                            foreach (var row in rows)
                            {
                                var callRecord = new CallRecord
                                {
                                    Id = int.Parse(row.Cell(1).GetString()),
                                    Tarih = DateTime.Parse(row.Cell(2).GetString(), CultureInfo.InvariantCulture),
                                    Arayan = row.Cell(3).GetString(),
                                    Aranan = row.Cell(4).GetString(),
                                    ToplamSure = TimeSpan.Parse(row.Cell(5).GetString(), CultureInfo.InvariantCulture),
                                    BeklemeSuresi = TimeSpan.Parse(row.Cell(6).GetString(), CultureInfo.InvariantCulture),
                                    GorusmeSuresi = TimeSpan.Parse(row.Cell(7).GetString(), CultureInfo.InvariantCulture),
                                    Sonuc = int.Parse(row.Cell(8).GetString()),
                                    Tipi = short.Parse(row.Cell(9).GetString())
                                };
                                callRecords.Add(callRecord);
                            }

                            await InsertCallRecordsAsync(callRecords);
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


        private async Task InsertCallRecordsAsync(List<CallRecord> callRecords)
        {
            try
            {
                using (var connection = new SqlConnection("Server=192.168.100.220;Database=Dv;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;"))
                {
                    await connection.OpenAsync();
                    var maxId = await connection.ExecuteScalarAsync<int>("SELECT MAX(Id) FROM Dv_Destek_Cagrilar");
                    int newId = maxId + 1;

                    foreach (var callRecord in callRecords)
                    {


                        string query = @"
                    INSERT INTO dbo.Dv_Destek_Cagrilar (Tarih, Arayan, Aranan, ToplamSure, BeklemeSuresi, GorusmeSuresi, Sonuc, Tipi)
                    VALUES (@Tarih, @Arayan, @Aranan, @ToplamSure, @BeklemeSuresi, @GorusmeSuresi, @Sonuc, @Tipi)";

                        await connection.ExecuteAsync(query, callRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in InsertCallRecordsAsync: {ex.Message}");
                throw;
            }
        }

    }
}

