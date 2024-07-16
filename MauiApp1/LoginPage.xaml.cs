using Microsoft.Extensions.Logging;
using MauiApp1.Services;
using System;
using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Logging;
using MauiApp1.Services;

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
                    _logger.LogInformation("User {Email} logged in successfully.", email);
                    await DisplayAlert("Success", "Login successful", "OK");
                    // Navigate to the main page
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    _logger.LogWarning("Invalid login attempt for user {Email}.", email);
                    MessageLabel.Text = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user {Email}.", email);
                MessageLabel.Text = "An error occurred. Please try again.";
            }
        }
    }
}
