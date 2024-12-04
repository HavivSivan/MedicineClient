using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class FirstPage : ContentPage
{
	public FirstPage(FirstPageViewModel vm)
	{
		this.BindingContext=vm;
		InitializeComponent();
	}
}