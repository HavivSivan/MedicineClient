using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class OrderPage : ContentPage
{
	public OrderPage(OrderPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
    }
}