using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using MedicineClient.Models;
using ZXing;
using MedicineClient.Services;

namespace MedicineClient.ViewModels
{
    public class UploadPrescriptionPageViewModel : ViewModelBase
    {
        public Medicine selectedMedicine { get; set; }
        public string selectedMedicineName => selectedMedicine.MedicineName;
        private string fileName;
        private bool isUploaded;
        public bool IsUploaded { get => isUploaded; set { isUploaded = value; OnPropertyChanged(); } }
        public string FileName { get => fileName; set { fileName = value; OnPropertyChanged(); } }
         public Command UploadPrescriptionCommand { get; set; }
        public Command OrderCommand { get; set; }
        private MedicineWebApi proxy;
        public UploadPrescriptionPageViewModel(MedicineWebApi proxy)
        {
            UploadPrescriptionCommand = new Command(async () => await UploadPrescription());
            OrderCommand=new Command(async () => await OrderMed());
            this.proxy = proxy;
            FileName="";
        }
        public async Task Initialize(Medicine selectedmedicine)
        {
            this.selectedMedicine=selectedmedicine;
        }
        private async Task UploadPrescription()
        {
            var fileResult = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Please select a prescription",
                FileTypes = FilePickerFileType.Images
            });
            if(fileResult== null)
            {
                return;
            }   
            FileName = fileResult.FileName.ToString();
            using var stream = await fileResult.OpenReadAsync();
            var imageBytes = new byte[stream.Length];
            await stream.ReadAsync(imageBytes, 0, (int)stream.Length);

            string imageUrl = await proxy.UploadPrescriptionImage(imageBytes, fileResult.FileName);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                FileName = imageUrl;
                IsUploaded = true;
            }
            else
            {
                Console.WriteLine("Upload failed");
            }

        }
        private async Task OrderMed()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                await ((App)Application.Current).MainPage.DisplayAlert("Error", "Please upload a prescription first", "OK");
                return;
            }
            IsUploaded = true;
            bool result = await ((App)Application.Current).MainPage.DisplayAlert("Success", "Prescription uploaded successfully. Do you want to order the medicine?", "Yes", "No");
            if (result)
            {

                bool orderResult = await proxy.OrderMedicine(selectedMedicine, FileName);
                if (orderResult)
                {
                    await ((App)Application.Current).MainPage.DisplayAlert("Success", "Medicine ordered successfully", "OK");
                    await ((App)Application.Current).MainPage.Navigation.PopAsync();
                    await ((App)Application.Current).MainPage.Navigation.PopAsync();
                }
                else
                {
                    await ((App)Application.Current).MainPage.DisplayAlert("Failure", "Failed to order medicine", "OK");
                }
            }
        }

    }
}
