using Microsoft.Extensions.Logging;
using MauiApp1.Services;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App(DatabaseService databaseService, ILogger<LoginPage> logger)
        {
            InitializeComponent();

            MainPage = new LoginPage(databaseService, logger);

            MainPage = new NavigationPage(new LoginPage(databaseService, logger));
        }
    }
}

