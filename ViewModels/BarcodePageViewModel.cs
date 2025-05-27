using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MedicineClient.Models;
using MedicineClient.Services;
using MedicineClient.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace MedicineClient.ViewModels
{
    public class BarcodePageViewModel : ViewModelBase
    {
        private readonly DrugWebAPI proxy;
        private string barcode;
        private bool isDetecting;
        private ObservableCollection<Medicine> foundMedicines;
        private IServiceProvider serviceProvider;
        public BarcodeReaderOptions Options { get; private set; }
        public BarcodePageViewModel(IServiceProvider serviceProvider, DrugWebAPI proxy)
        {
            this.proxy = proxy;
            IsDetecting = true;
             this.serviceProvider=serviceProvider;
            SearchCommand = new Command(async () => await SearchAsync());
            BarcodeDetectedCommand = new Command<string>(OnBarcodeDetected);
            Options = new BarcodeReaderOptions
            {
                AutoRotate = true,
                TryHarder = true,
                Formats = BarcodeFormat.Ean13
            };

        }

        public string Barcode
        {
            get => barcode;
            set
            {
                if (barcode != value)
                {
                    barcode = value;
                    OnPropertyChanged();
                    ((Command)SearchCommand).ChangeCanExecute();
                }
            }
        }

        public bool IsDetecting
        {
            get => isDetecting;
            set
            {
                if (isDetecting != value)
                {
                    isDetecting = value;
                    OnPropertyChanged();
                }
            }
        }

        

        public bool CanSearch => !string.IsNullOrWhiteSpace(Barcode);

        public ICommand SearchCommand { get; }
        public ICommand BarcodeDetectedCommand { get; }

        public async Task SearchAsync()
        {
            try
            {
                IsDetecting = false;

                string result = await proxy.SearchByBarcode(Barcode);
                if (string.IsNullOrWhiteSpace(result))
                    return;

                var medicineApi = serviceProvider.GetService<MedicineWebApi>();
                if (medicineApi == null)
                    return;

                var listingVM = new ListingPageViewModel(medicineApi, serviceProvider);
                await listingVM.Initialize(result);

                var newListingPage = new ListingPage(listingVM);

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await ((App)Application.Current).MainPage.Navigation.PopAsync();
                    await ((App)Application.Current).MainPage.Navigation.PushAsync(newListingPage);
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in SearchAsync: " + ex.Message);
            }
        }




        public void OnBarcodeDetected(string result)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Barcode = result;
            });
        }

    }
}
