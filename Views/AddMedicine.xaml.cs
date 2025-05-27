using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class AddMedicine : ContentPage
{
	public AddMedicine(AddMedicineViewModel vm)
	{
		InitializeComponent();
		this.BindingContext=vm;
	}
}