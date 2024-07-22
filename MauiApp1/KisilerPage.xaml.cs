using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System;

using MauiApp1.Models;


namespace MauiApp1
{
    public partial class KisilerPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private int _currentPage = 1;
        private const int PageSize = 10;
        private int _totalPageCount;

        public ObservableCollection<Kisiler> Kisiler { get; set; }

        public KisilerPage()
        {
            InitializeComponent(); // XAML dosyasýndaki bileþenleri baþlatýr
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=MAUI;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            Kisiler = new ObservableCollection<Kisiler>();
            KisilerCollectionView.ItemsSource = Kisiler; // CollectionView'a veri kaynaðý atama
            LoadKisilerAsync();
        }
        public KisilerPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            Kisiler = new ObservableCollection<Kisiler>();
            BindingContext = this; // BindingContext ayarlandýðýndan emin olun
            LoadKisilerAsync();
        }


        private async void OnPreviousClicked(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadKisilerAsync();
            }
        }

        private async void OnNextClicked(object sender, EventArgs e)
        {
            if (_currentPage < _totalPageCount)
            {
                _currentPage++;
                await LoadKisilerAsync();
            }
        }

        private async Task LoadKisilerAsync()
        {
            try
            {
                var kisiler = await _databaseService.GetKisilerAsync(_currentPage, PageSize);
                var totalKisiler = await _databaseService.GetTotalKisilerCountAsync();
                _totalPageCount = (int)Math.Ceiling(totalKisiler / (double)PageSize);

                Kisiler.Clear();
                foreach (var kisi in kisiler)
                {
                    Kisiler.Add(kisi);
                }

                PageInfoLabel.Text = $"Page {_currentPage} of {_totalPageCount}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadKisilerAsync: {ex.Message}");
                await DisplayAlert("Error", "Failed to load persons. Please try again later.", "OK");
            }
        }
    }
}

