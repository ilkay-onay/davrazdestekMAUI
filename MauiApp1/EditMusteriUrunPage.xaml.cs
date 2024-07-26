using Microsoft.Maui.Controls;
using MauiApp1.Services;
using System;

namespace MauiApp1
{
    public partial class EditMusteriUrunPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public EditMusteriUrunPage(DvDestekMusteriUrun urun, DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            BindingContext = urun;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var urun = BindingContext as DvDestekMusteriUrun;
            if (urun != null)
            {
                try
                {
                    _databaseService.UpdateDvDestekMusteriUrun(urun);
                    await DisplayAlert("Başarılı", "Müşteri Ürün Güncellendi", "OK");
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in OnSaveButtonClicked: {ex.Message}");
                    await DisplayAlert("Hata", "Müşteri Ürün Güncellenemedi", "Devam");
                }
            }
        }
    }
}
