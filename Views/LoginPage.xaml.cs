using MedicineClient.ViewModels;
using MedicineClient.Views;
namespace MedicineClient.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel context)
	{
		this.BindingContext =context;
		InitializeComponent();
	}
}