using Microsoft.Extensions.Logging;
using MauiApp1.Services;

namespace MauiApp1

{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            string connectionString = "Server=192.168.100.220;Database=MAUI;Encrypt=True;TrustServerCertificate=True;User Id=sa;Password=Password1;";
            builder.Services.AddSingleton(new DatabaseService(connectionString));
            builder.Services.AddLogging(logging =>
            {
                logging.AddConsole();
            });

            return builder.Build();
        }
    }
}
