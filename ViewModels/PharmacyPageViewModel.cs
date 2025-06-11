using MedicineClient.Services;
using MedicineClient.Models;
using MedicineClient.ViewModels;
using System.Collections.ObjectModel;
using MedicineClient.Views;
using System.ComponentModel;
using System.Windows.Input;
namespace MedicineClient.ViewModels
{
    public class PharmacyPageViewModel : ViewModelBase
    {
        private MedicineWebApi proxy;

        public ObservableCollection<Medicine> Medicines { get; } = new();
        public ObservableCollection<Order> Orders { get; } = new();

        private string _response;
        public string Response
        {
            get => _response;
            set { _response = value; OnPropertyChanged(); }
        }

        string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage=value; OnPropertyChanged(); }
        }
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set { isRefreshing = value; OnPropertyChanged(); }
        }

        public Command ApproveCommand { get; }
        public Command DenyCommand { get; }
        public Command ApproveOrderCommand { get; }
        public Command DenyOrderCommand { get; }
        public Command AddMedicineCommand { get; }
        public Command RefreshCommand { get; }
        private IServiceProvider service;

        public PharmacyPageViewModel(MedicineWebApi proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.service= serviceProvider;
            IsRefreshing = false;
            ApproveCommand      = new Command<Medicine>(async m => await ChangeMedicineStatusAsync(m, "Approved"));
            DenyCommand         = new Command<Medicine>(async m => await ChangeMedicineStatusAsync(m, "Denied"));
            ApproveOrderCommand = new Command<Order>(async o => await ChangeOrderStatusAsync(o, "Approved"));
            DenyOrderCommand    = new Command<Order>(async o => await ChangeOrderStatusAsync(o, "Denied"));
            AddMedicineCommand = new Command(OnAddMedicine);
            RefreshCommand = new Command(async () => await LoadDataAsync());
            _ = LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            if (IsRefreshing) return;

            IsRefreshing = true;
            StatusMessage = string.Empty;
            try
            {
                await LoadMedicinesAsync();
                await LoadOrdersAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error refreshing: {ex.Message}";
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        public async void OnAddMedicine()
        {
            ((App)Application.Current).MainPage.Navigation.PushAsync(service.GetService<AddMedicine>());
        }
        private async Task LoadMedicinesAsync()
        {
            var all = await proxy.GetMedicinesAsync();
            Medicines.Clear();

            if (all == null || all.Count == 0)
            {
                Response = "You have no medicines to review.";
                return;
            }
            foreach (var m in all.Where(x => x.Status?.Mstatus == "Checking"))
                Medicines.Add(m);

            Response = Medicines.Any()
                ? string.Empty
                : "No medicines in “Checking” state.";
        }

        private async Task LoadOrdersAsync()
        {
            var all = await proxy.GetOrdersList();
            Orders.Clear();

            if (all == null || all.Count == 0)
            {
                Response = "You have no orders to review.";
                return;
            }

            foreach (var o in all.Where(x => x.OStatus == "Pending"))
            {
                Orders.Add(o);
                Console.WriteLine(o.PrescriptionImage);
            }
            Response = Orders.Any()
                ? string.Empty
                : "No pending orders.";
        }


        async Task ChangeMedicineStatusAsync(Medicine m, string newStatus)
        {
            m.Status.Mstatus = newStatus;
            var ok = await proxy.UpdateMedicineAsync(m);
            StatusMessage = ok ? $"{m.MedicineName} {newStatus.ToLower()} successfully." : $"Failed to {newStatus.ToLower()} {m.MedicineName}.";
            if (ok) await LoadMedicinesAsync();
        }

        async Task ChangeOrderStatusAsync(Order o, string newStatus)
        {
            o.OStatus = newStatus;

            var ok = await proxy.UpdateOrderStatus(o);
            StatusMessage = ok
                ? $"Order {o.Id} {newStatus.ToLower()}."
                : $"Failed to {newStatus.ToLower()} order {o.Id}.";

            if (ok) await LoadOrdersAsync();
        }
    }

}