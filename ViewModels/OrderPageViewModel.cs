using MedicineClient.Models;
using MedicineClient.Services;
using MedicineClient.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace MedicineClient.ViewModels
{
    public class OrderPageViewModel : ViewModelBase 
    {
        public Medicine SelectedMedicine { get; set; }
        private MedicineWebApi proxy;
        public Command OrderCommand { get; }
        private IServiceProvider service;
        public OrderPageViewModel(MedicineWebApi proxy, IServiceProvider serviceProvider)
        {

            this.proxy = proxy;
            OrderCommand = new Command(async () => await OnOrder());
            service=serviceProvider;
            
        }
        public async Task Initialize(Medicine selectedmedicine)
        {
            this.SelectedMedicine=selectedmedicine;
        }
        public async Task OnOrder()
        {
            if (SelectedMedicine != null)
            {
                if (!SelectedMedicine.NeedsPrescription)
                {
                    bool result = await proxy.OrderMedicine(SelectedMedicine, "");
                    if (result)
                    {
                        await ((App)Application.Current).MainPage.DisplayAlert("Success", "Medicine ordered successfully", "OK");
                        await ((App)Application.Current).MainPage.Navigation.PushAsync(service.GetService<ListingPage>());
                        return;
                    }
                    await ((App)Application.Current).MainPage.DisplayAlert("Failure", "Medicine doesn't exist", "OK");
                    await ((App)Application.Current).MainPage.Navigation.PopAsync();
                }
                else
                {
                    bool response = await ((App)Application.Current).MainPage.DisplayAlert("Prescription Required", "This medicine requires a prescription. Do you want to upload a prescription?", "Yes", "No");
                    if (response)
                    {
                        UploadPrescriptionPageViewModel vm = new UploadPrescriptionPageViewModel(proxy);
                        UploadPrescriptionPage uploadPage = new UploadPrescriptionPage(vm, SelectedMedicine);
                        await ((App)Application.Current).MainPage.Navigation.PushAsync(uploadPage);

                    }
                    else
                    {
                        await ((App)Application.Current).MainPage.Navigation.PopAsync();
                        return;
                    } 
                }
            }
        }
    }
}
