using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MauiApp1.Services;

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

                // BindingContext'in veriler yüklendikten sonra ayarlanması
                BindingContext = this;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadMusteriUrunAsync: {ex.Message}");
                await DisplayAlert("Hata", "Müşteri Ürün Yüklenemedi", "Devam");
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
    }
}
