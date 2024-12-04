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
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<RegisterPageViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddSingleton<MedicineWebApi>();
            builder.Services.AddSingleton<ShellViewModel>();
            builder.Services.AddSingleton<FirstPage>();
            builder.Services.AddSingleton<FirstPageViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
