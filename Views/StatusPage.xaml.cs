using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class StatusPage : ContentPage
{
	public StatusPage(StatusPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as StatusPageViewModel)?.LoadData();
    }
}