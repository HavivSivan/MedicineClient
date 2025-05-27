using MedicineClient.ViewModels;
using MedicineClient.Views;
using MedicineClient.Services;
namespace MedicineClient.Views;

public partial class PharmacyPage : ContentPage
{
	public PharmacyPage(PharmacyPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
    }
}
