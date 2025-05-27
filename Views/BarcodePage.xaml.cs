using MedicineClient.ViewModels;
using ZXing.Net.Maui;
namespace MedicineClient.Views;

public partial class BarcodePage : ContentPage
{
	public BarcodePage(BarcodePageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext=vm;
	}

    private void BarcodeReader_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var resultText = e.Results?.FirstOrDefault()?.Value;

        if (!string.IsNullOrWhiteSpace(resultText) && BindingContext is BarcodePageViewModel vm)
        {
            if (vm.BarcodeDetectedCommand?.CanExecute(resultText) == true)
            {
                vm.BarcodeDetectedCommand.Execute(resultText);
            }
        }
    }



}