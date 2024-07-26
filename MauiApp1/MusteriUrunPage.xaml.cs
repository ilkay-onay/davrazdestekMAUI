using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MauiApp1.Services;
using MauiApp1.Models;

namespace MauiApp1
{
    public partial class MusteriUrunPage : ContentPage, INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;

        private ObservableCollection<DvDestekMusteriUrun> _musteriUrunList;
        public ObservableCollection<DvDestekMusteriUrun> MusteriUrunList
        {
            get => _musteriUrunList;
            set
            {
                _musteriUrunList = value;
                OnPropertyChanged(nameof(MusteriUrunList));
            }
        }

        public MusteriUrunPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=Dv;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            MusteriUrunList = new ObservableCollection<DvDestekMusteriUrun>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadMusteriUrunAsync();
        }

        private async Task LoadMusteriUrunAsync()
        {
            try
            {
                var urunList = await _databaseService.GetAllDvDestekMusteriUrunAsync();
                MusteriUrunList.Clear();
                foreach (var urun in urunList)
                {
                    MusteriUrunList.Add(urun);
                }
                BindingContext = this;
            }


            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadMusteriUrunAsync: {ex.Message}");
                await DisplayAlert("Hata", "Müşteri Ürün Yüklenemedi", "Devam");
            }
        }
        private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            await SearchUrunAsync(e.NewTextValue);
        }

        private async Task SearchUrunAsync(string searchTerm)
        {
            try
            {
                var searchResults = await _databaseService.SearchUrunAsync(searchTerm);

                MusteriUrunList.Clear();
                foreach (var urun in searchResults)
                {
                    MusteriUrunList.Add(urun);
                }
                BindingContext = this;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchKisilerAsync: {ex.Message}");
                await DisplayAlert("Error", "Failed to search persons. Please try again later.", "OK");
            }
        }
    }
}
