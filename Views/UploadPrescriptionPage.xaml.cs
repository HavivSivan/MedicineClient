using MedicineClient.ViewModels;
namespace MedicineClient.Views;

public partial class UploadPrescriptionPage : ContentPage
{
	public UploadPrescriptionPage(UploadPrescriptionPageViewModel vm)
	{	
		InitializeComponent();
        this.BindingContext=vm;
    }
}