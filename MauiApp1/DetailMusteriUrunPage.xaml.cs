using MauiApp1.Services;
using System;

namespace MauiApp1
{
    public partial class DetailMusteriUrunPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public DetailMusteriUrunPage(DvDestekMusteriUrunDetay urunDetay, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            BindingContext = urunDetay;
        }
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var urunDetay = BindingContext as DvDestekMusteriUrunDetay;
            if (urunDetay != null)
            {
                try
                {
                    await _databaseService.UpdateDvDestekMusteriUrunDetayAsync(urunDetay);
                    await DisplayAlert("Ba�ar�l�", "M��teri �r�n G�ncellendi", "OK");
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in OnSaveButtonClicked: {ex.Message}");
                    await DisplayAlert("Hata", "M��teri �r�n G�ncellenemedi", "Devam");
                }
            }
        }


    }
    }

