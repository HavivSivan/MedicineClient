using MedicineClient.ViewModels;
namespace MedicineClient.ViewModels;

public partial class PharmacyPage : ContentPage
{
	public PharmacyPage(PharmacyPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext=vm;
	}
}