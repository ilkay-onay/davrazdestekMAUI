using Microsoft.Extensions.Logging;
using MauiApp1.Services;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static DatabaseService DatabaseServiceInstance { get; private set; }
        public static ILoggerFactory LoggerFactoryInstance { get; private set; }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            string connectionString = "Server=192.168.100.220;Database=dv;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;";
            DatabaseServiceInstance = new DatabaseService(connectionString);
            LoggerFactoryInstance = LoggerFactory.Create(logging =>
            {
                logging.AddConsole();
            });

            builder.Services.AddSingleton(DatabaseServiceInstance);
            builder.Services.AddSingleton(LoggerFactoryInstance);

            return builder.Build();
        }
    }
}
