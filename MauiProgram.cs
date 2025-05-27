using Microsoft.Extensions.Logging;
using MedicineClient.ViewModels;
using MedicineClient.Views;
using MedicineClient.Services;
using ZXing.Net.Maui.Controls;
namespace MedicineClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseBarcodeReader()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Gurajada-Regular.ttf", "Gurajada");
                });
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<AdminPage>();
            builder.Services.AddTransient<ListingPage>();
            builder.Services.AddTransient<PharmacyPage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<StatusPage>();
            builder.Services.AddTransient<BarcodePage>();
            builder.Services.AddTransient<OrderPage>();
            builder.Services.AddTransient<UploadPrescriptionPage>();
            builder.Services.AddTransient<AddMedicine>();
            builder.Services.AddTransient<AddPharmacy>();
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<RegisterPageViewModel>();
            builder.Services.AddTransient<AdminPageViewModel>();
            builder.Services.AddTransient<ListingPageViewModel>();
            builder.Services.AddTransient<PharmacyPageViewModel>();
            builder.Services.AddTransient<ProfilePageViewModel>();
            builder.Services.AddTransient<StatusPageViewModel>();
            builder.Services.AddTransient<BarcodePageViewModel>();
            builder.Services.AddTransient<OrderPageViewModel>();
            builder.Services.AddTransient<UploadPrescriptionPageViewModel>();
            builder.Services.AddTransient<AddMedicineViewModel>();
            builder.Services.AddTransient<AddPharmacyViewModel>();
            builder.Services.AddSingleton<MedicineWebApi>();


#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            return builder.Build();
        }
    }
}
