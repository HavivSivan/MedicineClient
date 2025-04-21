using MedicineClient.ViewModels;
namespace MedicineClient.Views;


public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		this.BindingContext =vm;
		InitializeComponent();
	}
}