﻿using MedicineClient.Services;
using MedicineClient.Models;
using MedicineClient.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace MedicineClient.ViewModels
{
    public class PharmacyPageViewModel : ViewModelBase
    {
        private MedicineWebApi proxy;
        public ObservableCollection<Medicine> Medicines { get; set; } = new();
        private string response;
        public string Response
        { get=>response; set { response=value; OnPropertyChanged(); } }
        public Command ApproveCommand { get; }
        public Command DenyCommand { get; }

        private string statusMessage;
        public string StatusMessage
        {
            get => statusMessage;
            set { statusMessage = value; OnPropertyChanged(); }
        }

        public PharmacyPageViewModel(MedicineWebApi proxy)
        {
            Response ="";
            this.proxy = proxy;
            ApproveCommand = new Command<Medicine>(async (medicine) => await UpdateMedicineStatus(medicine, "Approved"));
            DenyCommand = new Command<Medicine>(async (medicine) => await UpdateMedicineStatus(medicine, "Denied"));
            LoadMedicines();

        }

        private async Task LoadMedicines()
        {
            var medicines = await proxy.GetMedicineList();
            Medicines.Clear();
            foreach (var medicine in medicines)
            {
                Medicines.Add(medicine);
                if (Medicines.Count == 0)
                {
                    Response="You have no medicine";
                    OnPropertyChanged(nameof(Response));
                }
                Medicines.Add(new Medicine
                {
                    MedicineName = "TestMed",
                    BrandName = "TestBrand",
                    Status = new MedicineStatus { Mstatus = "Pending", Notes = "" },
                    Pharmacy = new Pharmacy { Name = "TestPharmacy", Adress = "Test St", Phone = "000", User = new AppUser() },
                    user = new AppUser()
                });
            }
        }

        private async Task UpdateMedicineStatus(Medicine medicine, string status)
        {
            medicine.Status.Mstatus = status;
            var success = await proxy.UpdateMedicineAsync(medicine);
            StatusMessage = success ? $"{medicine.MedicineName} updated successfully." : "Failed to update medicine.";
            if (success) await LoadMedicines();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}