using Microsoft.Extensions.Logging;
using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace MauiApp1
{
    public partial class MainPage : TabbedPage              
    {
        private readonly DatabaseService _databaseService;
        private readonly ILogger<MainPage> _logger;

        public MainPage(DatabaseService databaseService, ILogger<MainPage> logger)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _logger = logger;

            // Settings tab'ı için kullanıcı bilgilerini ayarla
            var settingsPage = this.Children[4] as ContentPage;
            if (settingsPage != null)
            {
                settingsPage.BindingContext = new DvDestekPersonel
                {
                    Id = Preferences.Get("UserId", 0),
                    Ad_Soyad = Preferences.Get("UserName", "N/A"),
                    E_posta = Preferences.Get("UserEmail", "N/A"),
                    Telefon = Preferences.Get("UserPhone", "N/A"),
                    Dahili = Preferences.Get("UserDahili", "N/A"),
                    Sifre = Preferences.Get("UserPassword", "N/A")
                };
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<LoginPage>();

            await DisplayAlert("Çıkış Yapıldı", "", "Tamam");
            Application.Current.MainPage = new LoginPage(_databaseService, logger);
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            var settingsPage = this.Children[4] as ContentPage;
            if (settingsPage != null && settingsPage.BindingContext is DvDestekPersonel dvDestekPersonel)
            {
                //await updateDvDestekPersonel(dvDestekPersonel);
                await DisplayAlert("Başarılı", "Bilgiler güncellendi", "Tamam");

                // Güncellenmiş bilgileri Preferences'e kaydet
                Preferences.Set("UserName", dvDestekPersonel.Ad_Soyad);
                Preferences.Set("UserEmail", dvDestekPersonel.E_posta);
                Preferences.Set("UserPhone", dvDestekPersonel.Telefon);
                Preferences.Set("UserDahili", dvDestekPersonel.Dahili);
                Preferences.Set("UserPassword", dvDestekPersonel.Sifre);
            }
        }

       


    }

    
}
