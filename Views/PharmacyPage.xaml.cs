using MedicineClient.ViewModels;
using MedicineClient.Views;
using MedicineClient.Services;
namespace MedicineClient.Views;

public partial class PharmacyPage
{
	public PharmacyPage()
	{
		InitializeComponent();
        BindingContext = new PharmacyPageViewModel(new MedicineWebApi());
    }
}
