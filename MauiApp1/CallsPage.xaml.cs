using MauiApp1.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System;
using MauiApp1.Services;

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
        public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-1); // Default to last month
        public DateTime EndDate { get; set; } = DateTime.Today; // Default to today

        public CallsPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=Dv;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            Calls = new ObservableCollection<CallRecord>();
            UniqueArananList = new ObservableCollection<string>();
            BindingContext = this;
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
            _selectedAranan = ArananPicker.SelectedItem as string;
            await LoadCallsAsync();
        }

        private async void OnResetClicked(object sender, EventArgs e)
        {
            StartDate = DateTime.Today.AddMonths(-1); // Reset to last month
            EndDate = DateTime.Today; // Reset to today
            ArananPicker.SelectedItem = null; // Reset dropdown selection
            SearchBar.Text = string.Empty; // Clear search bar
            _selectedAranan = null; // Clear selected "Aranan"
            await LoadCallsAsync();
        }

        private async Task LoadCallsAsync(string searchQuery = "")
        {
            try
            {
                var calls = await _databaseService.GetCallsAsync(_currentPage, PageSize, StartDate, EndDate, _selectedAranan, searchQuery);
                var totalCalls = await _databaseService.GetTotalCallCountAsync(StartDate, EndDate, _selectedAranan, searchQuery);
                _totalPageCount = (int)Math.Ceiling(totalCalls / (double)PageSize);

                Calls.Clear();
                foreach (var call in calls)
                {
                    Calls.Add(call);
                }

                PageInfoLabel.Text = $"Sayfa {_currentPage} / {_totalPageCount}";
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
    }
}
