using MedicineClient.ViewModels;
using SweetBoxApp.ViewModels;
namespace MedicineClient.Views;


public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel context)
	{
		this.BindingContext =context;
		InitializeComponent();
	}
}