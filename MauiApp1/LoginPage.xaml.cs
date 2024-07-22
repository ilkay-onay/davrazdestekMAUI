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
                MessageLabel.Text = "Please enter email and password.";
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
                        _logger?.LogInformation("User {Email} logged in successfully.", email);
                        await DisplayAlert("Başarıyla Giriş Yapıldı!", "", "Tamam");

                        // Kullanıcı bilgilerini Preferences'a kaydet
                        Preferences.Set("UserName", user.Ad_Soyad);
                        Preferences.Set("UserEmail", user.E_posta);
                        Preferences.Set("UserPhone", user.Telefon);
                        Preferences.Set("UserDahili", user.Dahili);

                        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                        var logger = loggerFactory.CreateLogger<MainPage>();

                        // Navigate to the MainPage
                        var mainPage = new MainPage(_databaseService, logger);
                        Application.Current.MainPage = new NavigationPage(mainPage);
                    }
                    else
                    {
                        _logger?.LogWarning("Failed to retrieve user information for {Email}.", email);
                        MessageLabel.Text = "An error occurred. Please try again.";
                    }
                }
                else
                {
                    _logger?.LogWarning("Invalid login attempt for user {Email}.", email);
                    MessageLabel.Text = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "An error occurred while logging in user {Email}.", email);
                MessageLabel.Text = "An error occurred. Please try again.";
            }
        }
    }
}