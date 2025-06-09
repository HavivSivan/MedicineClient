using MedicineClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicineClient.Models;

namespace MedicineClient.ViewModels
{
    public class AddPharmacyViewModel : ViewModelBase
    {
        public Command AddPharmacyCommand { get; }
        public MedicineWebApi proxy;
        public AddPharmacyViewModel(MedicineWebApi proxy)
        {
            this.proxy=proxy;
            PharmacyName = string.Empty;
            Address = string.Empty;
            Phone = string.Empty;
            Id = 0;
            StatusMessage = string.Empty;
            AddPharmacyCommand=new Command(async () => OnAddPharmacy());
        }
        private string pharmacyName;
        private string address;
        private string phone;
        private int id;
        private string statusMessage;
        public string PharmacyName
        {
            get => pharmacyName;
            set { pharmacyName = value; OnPropertyChanged(); }
        }
        public string Address
        {
            get => address;
            set { address = value; OnPropertyChanged(); }
        }
        public string Phone
        {
            get => phone;
            set { phone = value; OnPropertyChanged(); }
        }
        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }
        public string StatusMessage
        {
            get => statusMessage;
            set { statusMessage = value; OnPropertyChanged(); }
        }
        private async Task OnAddPharmacy()
        {
            if (Address != string.Empty && PharmacyName != string.Empty && Phone != string.Empty && Id != 0)
            {
                if (proxy == null)
                {
                    StatusMessage = "Internal error: Service unavailable.";
                    return;
                }

                PharmacyCreateDTO pharm = new PharmacyCreateDTO
                {
                    Name = PharmacyName,
                    Address = Address,
                    Phone = Phone,
                    UserId=Id
                };
                try
                {
                    bool result = await proxy.AddPharmacy(pharm);
                    if (result)
                        StatusMessage = "Pharmacy added successfully!";
                    else
                        StatusMessage = "Failed to add pharmacy. Please try again.";
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Error adding pharmacy: {ex.Message}";
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
