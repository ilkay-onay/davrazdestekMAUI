using Microsoft.Extensions.Logging;
using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System;

namespace MauiApp1
{
    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly ILogger<LoginPage> _logger;
        private bool _isPasswordVisible = false;

        public LoginPage(DatabaseService databaseService, ILogger<LoginPage> logger)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _logger = logger;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageLabel.Text = "Lütfen e-posta ve şifreyi girin.";
                return;
            }

            try
            {
                var isValidUser = await _databaseService.ValidateUserAsync(email, password);
                if (isValidUser)
                {
                    var user = await _databaseService.GetUserInfo(email, password);
                    if (user != null)
                    {
                        _logger?.LogInformation("{Email} kullanıcısı başarıyla giriş yaptı", email);
                        await DisplayAlert("Başarıyla Giriş Yapıldı!", "", "Tamam");

                        // Kullanıcı bilgilerini Preferences'a kaydet
                        Preferences.Set("UserName", user.Ad_Soyad);
                        Preferences.Set("UserEmail", user.E_posta);
                        Preferences.Set("UserPhone", user.Telefon);
                        Preferences.Set("UserDahili", user.Dahili);
                        Preferences.Set("UserPassword", user.Sifre);


                        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                        var logger = loggerFactory.CreateLogger<MainPage>();

                        // Navigate to the MainPage
                        var mainPage = new MainPage(_databaseService, logger);
                        Application.Current.MainPage = new NavigationPage(mainPage);
                    }
                    else
                    {
                        _logger?.LogWarning("{Email} için kullanıcı bilgileri alınamadı.", email);
                        MessageLabel.Text = "Bir hata oluştu. Lütfen tekrar deneyin.";
                    }
                }
                else
                {
                    _logger?.LogWarning("{Email} kullanıcısı için geçersiz giriş denemesi.", email);
                    MessageLabel.Text = "Geçersiz e-posta veya şifre.";
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "{Email} kullanıcısı oturum açarken bir hata oluştu.", email);
                MessageLabel.Text = "Bir hata oluştu. Lütfen tekrar deneyin.";
            }
        }

        private void OnTogglePasswordVisibility(object sender, EventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;
            PasswordEntry.IsPassword = !_isPasswordVisible;
            TogglePasswordButton.Source = _isPasswordVisible ? "eyeacik.png" : "eyekapali.png";
        }
    }
}
