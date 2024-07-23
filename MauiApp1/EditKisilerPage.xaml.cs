using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System;

namespace MauiApp1
{
    public partial class EditKisilerPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public Kisiler Kisiler { get; set; }

        public EditKisilerPage(Kisiler kisi, DatabaseService databaseService)
        {
            InitializeComponent();
            Kisiler = kisi;
            _databaseService = databaseService;
            BindingContext = this;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                await _databaseService.UpdateKisilerAsync(Kisiler);
                await DisplayAlert("Success", "Kişi bilgileri güncellendi.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnSaveClicked: {ex.Message}");
                await DisplayAlert("Error", "Kişi bilgileri güncellenemedi. Lütfen tekrar deneyin.", "OK");
            }
        }
    }
}
