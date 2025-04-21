using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}