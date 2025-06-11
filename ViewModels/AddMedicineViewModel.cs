using MedicineClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicineClient.Models;

namespace MedicineClient.ViewModels
{
    public class AddMedicineViewModel : ViewModelBase
    {
        private MedicineWebApi proxy;
        public Command AddMedicineCommand { get; }
        public AddMedicineViewModel(MedicineWebApi proxy)
        {
            this.proxy= proxy;
            AddMedicineCommand = new Command(async () => OnAddMedicine());
        }
        private string medicineName;
        private string brandName;
        private string username;
        private string statusMessage;
        public string MedicineName
        {
            get => medicineName;
            set { medicineName = value; OnPropertyChanged(); }
        }
        public string BrandName
        {
            get => brandName;
            set { brandName = value; OnPropertyChanged(); }
        }
        public string Username
        {
            get => username;
            set { username = value; OnPropertyChanged(); }
        }
        public string StatusMessage
        {
            get => statusMessage;
            set { statusMessage = value; OnPropertyChanged(); }
        }
        private async Task OnAddMedicine()
        {
            if (medicineName != string.Empty && BrandName!= string.Empty && Username != string.Empty)
            {
                AppUser Sender = await proxy.GetUserByUsernameAsync(Username);
                if (proxy == null)
                {
                    StatusMessage = "Internal error: Service unavailable.";
                    return;
                }
                MedicineStatus status = new MedicineStatus
                {
                    Mstatus = "Pending"
                };
                Pharmacy pharmacy = new Pharmacy
                {
                    Id=proxy.GetPharmacy().Id
                };
                MedicineCreateDTO med = new MedicineCreateDTO
                {
                    MedicineName = MedicineName,
                    BrandName = BrandName,
                    PharmacyId = pharmacy.Id,
                    Userid= Sender.Id
                };
                try
                {
                    bool result = await proxy.AddMedicine(med);
                    if (result)
                        StatusMessage = "Medicine added successfully!";
                    else
                        StatusMessage = "Failed to add medicine. Please try again.";
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Error adding medicine: {ex.Message}";
                    return;
                }
            }
            else
            {
                StatusMessage = "All fields are required.";
                return;
            }
        }

    }
}
