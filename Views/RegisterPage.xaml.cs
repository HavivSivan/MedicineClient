using Microsoft.Win32;
using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel vm)
	{
		InitializeComponent();
            BindingContext = vm;
            
    }
}