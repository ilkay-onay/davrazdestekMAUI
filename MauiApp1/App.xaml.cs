using Microsoft.Extensions.Logging;
using MauiApp1.Services;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App(DatabaseService databaseService, ILogger<MainPage> logger)
        {
            InitializeComponent();

            // LoginPage'i NavigationPage içine sarıyoruz
            MainPage = new NavigationPage(new MainPage(databaseService, logger));
        }
    }
}
