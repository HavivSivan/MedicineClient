using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class BarcodePage : ContentPage
{
	public BarcodePage(BarcodePageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}