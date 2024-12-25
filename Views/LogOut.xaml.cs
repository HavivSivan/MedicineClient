using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class LogOut : ContentPage
{
	public LogOut(LogOutViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}