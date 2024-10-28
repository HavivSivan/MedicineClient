using MedicineClient.ViewModels;
namespace MedicineClient.Views;


public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel context)
	{
		this.BindingContext =context;
		InitializeComponent();
	}
}