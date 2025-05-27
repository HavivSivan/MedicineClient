using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class AddPharmacy : ContentPage
{
	public AddPharmacy(AddPharmacyViewModel vm)
	{
		InitializeComponent();
		this.BindingContext=vm;
    }
}