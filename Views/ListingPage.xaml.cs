using MedicineClient.ViewModels;

namespace MedicineClient.Views;

public partial class ListingPage : ContentPage
{
	public ListingPage(ListingPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext=vm;
	}
}