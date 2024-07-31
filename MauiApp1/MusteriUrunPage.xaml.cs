using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MauiApp1.Services;
using System.ComponentModel;

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

        public ObservableCollection<DvDestekUrunler> UrunList { get; set; }

        public MusteriUrunPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService("Server=192.168.100.220;Database=Dv;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;");
            MusteriUrunList = new ObservableCollection<DvDestekMusteriUrun>();
            UrunList = new ObservableCollection<DvDestekUrunler>();
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
                var urunler = await _databaseService.GetAllDvDestekUrunlerAsync();
                var musteriUrunler = await _databaseService.GetAllDvDestekMusteriUrunAsync();

                UrunList.Clear();
                foreach (var urun in urunler)
                {
                    UrunList.Add(urun);
                }

                MusteriUrunList.Clear();
                foreach (var musteriUrun in musteriUrunler)
                {
                    musteriUrun.Urun = UrunList.FirstOrDefault(u => u.Id == musteriUrun.UrunId)?.Urun;
                    MusteriUrunList.Add(musteriUrun);
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
                    urun.Urun = UrunList.FirstOrDefault(u => u.Id == urun.UrunId)?.Urun;
                    MusteriUrunList.Add(urun);
                }
                BindingContext = this;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchUrunAsync: {ex.Message}");
                await DisplayAlert("Error", "Failed to search persons. Please try again later.", "OK");
            }
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var urun = button?.BindingContext as DvDestekMusteriUrun;
            if (urun != null)
            {
                await Navigation.PushAsync(new EditMusteriUrunPage(urun, _databaseService));
            }
        }
        private async void OnDetailButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var urun = button?.BindingContext as DvDestekMusteriUrun;
            if (urun != null)
            {
                var urunDetay = await _databaseService.GetDvDestekMusteriUrunDetayByIdAsync(urun.Id);
                if (urunDetay != null)
                {
                    await Navigation.PushAsync(new DetailMusteriUrunPage(urunDetay, _databaseService));
                }
            }
        }
    }
}
