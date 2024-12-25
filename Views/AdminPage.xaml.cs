using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext=vm;
	}
}