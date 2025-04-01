using MedicineClient.ViewModels;
using MedicineClient.Services;
namespace MedicineClient.Views;

public partial class ListingPage : ContentPage
{
    public ListingPage()
    {
        InitializeComponent();
        var proxy = App.Services.GetService<MedicineWebApi>();
        BindingContext = new ListingPageViewModel(proxy);
    }
}