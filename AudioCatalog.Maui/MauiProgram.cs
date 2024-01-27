using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sudzinski.AudioCatalog.MAUI.ViewModels;

namespace Sudzinski.AudioCatalog.MAUI
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            builder.Configuration.AddConfiguration(config);

            string libraryName = builder.Configuration["DAOLibraryName"];
            string assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, libraryName);

            BLC.BLC blc = new BLC.BLC(assemblyPath);
            builder.Services.AddSingleton(blc);

            builder.Services.AddSingleton<ProducersCollectionViewModel>();
            builder.Services.AddSingleton<ProducersPage>();
            builder.Services.AddSingleton<SpeakersCollectionViewModel>();
            builder.Services.AddSingleton<SpeakersPage>();

            return builder.Build();
        }
    }
}
