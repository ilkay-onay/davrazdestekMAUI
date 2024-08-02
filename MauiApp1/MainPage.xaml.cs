using Microsoft.Extensions.Logging;
using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class MainPage : TabbedPage
    {
        private bool _isPasswordVisible = false;
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
                try
                {
                    await UpdateDvDestekPersonelAsync(dvDestekPersonel);
                    await DisplayAlert("Başarılı", "Bilgiler güncellendi", "Tamam");

                    // Güncellenmiş bilgileri Preferences'e kaydet
                    Preferences.Set("UserName", dvDestekPersonel.Ad_Soyad);
                    Preferences.Set("UserEmail", dvDestekPersonel.E_posta);
                    Preferences.Set("UserPhone", dvDestekPersonel.Telefon);
                    Preferences.Set("UserDahili", dvDestekPersonel.Dahili);
                    Preferences.Set("UserPassword", dvDestekPersonel.Sifre);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating user information");
                    await DisplayAlert("Hata", "Bilgiler güncellenemedi", "Tamam");
                }
            }
        }

        private void OnTogglePasswordVisibility(object sender, EventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;
            Enr.IsPassword = !_isPasswordVisible;
            TogglePasswordButton.Source = _isPasswordVisible ? "eyeacik.png" : "eyekapali.png";
        }

        private async Task UpdateDvDestekPersonelAsync(DvDestekPersonel dvDestekPersonel)
        {
            await _databaseService.UpdateDvDestekPersonelAsync(dvDestekPersonel);
        }
    }
}
