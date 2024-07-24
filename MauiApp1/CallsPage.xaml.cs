using MauiApp1.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;
using MauiApp1.Services;

namespace MauiApp1
{
    public partial class CallsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private int _currentPage = 1;
        private const int PageSize = 7;
        private int _totalPageCount;

        public ObservableCollection<CallRecord> Calls { get; set; }

        public CallsPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=MAUI;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            Calls = new ObservableCollection<CallRecord>();
            BindingContext = this;
            CallsCollectionView.ItemsSource = Calls;
            LoadCallsAsync();
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

        private async Task LoadCallsAsync()
        {
            try
            {
                var calls = await _databaseService.GetCallsAsync(_currentPage, PageSize);
                var totalCalls = await _databaseService.GetTotalCallCountAsync();
                _totalPageCount = (int)Math.Ceiling(totalCalls / (double)PageSize);

                Calls.Clear();
                foreach (var call in calls)
                {
                    Calls.Add(call);
                }

                PageInfoLabel.Text = $"Page {_currentPage} of {_totalPageCount}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadCallsAsync: {ex.Message}");
                await DisplayAlert("Error", "Failed to load calls. Please try again later.", "OK");
            }
        }


    }
}
