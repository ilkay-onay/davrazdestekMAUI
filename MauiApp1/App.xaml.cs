using Microsoft.Extensions.Logging;
using MauiApp1.Services;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App(DatabaseService databaseService, ILogger<LoginPage> logger)
        {
            InitializeComponent();

<<<<<<< HEAD

            MainPage = new LoginPage(databaseService, logger);

            MainPage = new NavigationPage(new LoginPage(databaseService, logger));

=======
            MainPage = new LoginPage(databaseService, logger);
            MainPage = new NavigationPage(new LoginPage(databaseService, logger));
>>>>>>> ceac3868d98f783e6deb2922f2d96903ee2aaa9e
        }
    }
}

