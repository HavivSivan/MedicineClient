using Microsoft.Extensions.Logging;
using MedicineClient.ViewModels;
using MedicineClient.Views;
using MedicineClient.Services;
namespace MedicineClient
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
                    fonts.AddFont("Gurajada-Regular.ttf", "Gurajada");
                });
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<RegisterPageViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddSingleton<MedicineWebApi>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
