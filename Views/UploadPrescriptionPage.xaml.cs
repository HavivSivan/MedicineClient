using MedicineClient.ViewModels;
using MedicineClient.Models;
namespace MedicineClient.Views;

public partial class UploadPrescriptionPage : ContentPage
{
	public UploadPrescriptionPage(UploadPrescriptionPageViewModel vm, Medicine SelectedMedicine)
	{	
		InitializeComponent();
        vm.Initialize(SelectedMedicine);
        this.BindingContext=vm;
    }
}