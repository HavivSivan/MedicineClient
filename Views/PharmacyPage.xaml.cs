using MedicineClient.ViewModels;
using MedicineClient.Views;
namespace MedicineClient.Views;

public partial class PharmacyPage
{
	public PharmacyPage(PharmacyPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext=vm;
	}
}
