using Microsoft.Extensions.Logging;
using MauiApp1.Services;
using Microsoft.Maui.Controls;
using System;


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
            var settingsPage = this.Children[3] as ContentPage;
            if (settingsPage != null)
            {
                settingsPage.BindingContext = new
                {
                    UserName = Preferences.Get("UserName", "N/A"),
                    UserEmail = Preferences.Get("UserEmail", "N/A"),
                    UserPhone = Preferences.Get("UserPhone", "N/A"),
                    UserDahili = Preferences.Get("UserDahili", "N/A")
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
    }
}