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
            builder.Services.AddSingleton<LogOutViewModel>();
            builder.Services.AddSingleton<LogOut>();
            builder.Services.AddSingleton<AdminPageViewModel>();
            builder.Services.AddTransient<AdminPage>();
            builder.Services.AddTransient<ListingPage>();
            builder.Services.AddSingleton<PharmacyPage>();
            builder.Services.AddTransient<PharmacyPageViewModel>();
            builder.Services.AddSingleton<MedicineWebApi>();
            builder.Services.AddTransient<ListingPageViewModel>();
            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddTransient<ProfilePageViewModel>();
            builder.Services.AddTransient<StatusPage>();
            builder.Services.AddTransient<StatusPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            return builder.Build();
        }
    }
}
