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
