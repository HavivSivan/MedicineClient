using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class StatusPage : ContentPage
{
	public StatusPage(StatusPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}