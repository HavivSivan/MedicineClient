using System.Collections.ObjectModel;
using MedicineClient.Models;
using MedicineClient.Services;
using MedicineClient.ViewModels;
using MedicineClient;
namespace MedicineClient.ViewModels
{ 
    public class StatusPageViewModel : ViewModelBase
    {
    private MedicineWebApi proxy;

    public ObservableCollection<Medicine> MyMedicines { get; set; } = new();
    public ObservableCollection<Medicine> FilteredMedicines { get; set; } = new();

    public ObservableCollection<Order> MyOrders { get; set; } = new();
    public ObservableCollection<Order> FilteredOrders { get; set; } = new();

    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                OnPropertyChanged();
                FilterData();
            }
        }
    }

    public StatusPageViewModel()
    {
        proxy = new MedicineWebApi();
    }

        public async void LoadData()
        {
            try
            {
                var allMedicines = await proxy.GetUserMedicinesAsync();
                Console.WriteLine($"fetched {allMedicines.Count} medicines");
                var allOrders = await proxy.GetUserOrdersAsync();
                Console.WriteLine($"fetched {allOrders.Count} orders");

                var currentUser = ((App)Application.Current).LoggedInUser;
                MyMedicines.Clear();
                foreach (var m in allMedicines.Where(m => m.user.Id == currentUser.Id))
                    MyMedicines.Add(m);

                MyOrders.Clear();
                foreach (var o in allOrders.Where(o => o.User.Id == currentUser.Id))
                    MyOrders.Add(o);

                FilterData();
                Console.WriteLine($"filtered to {FilteredMedicines.Count} meds and {FilteredOrders.Count} orders");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadData: {ex}");
            }
        }


        public void FilterData()
    {
        string keyword = (SearchText ?? "").ToLower();

        var filteredMeds = MyMedicines.Where(m => m.MedicineName.ToLower().Contains(keyword) ||m.BrandName.ToLower().Contains(keyword)).ToList();

        var filteredOrds = MyOrders.Where(o => o.Medicine.MedicineName.ToLower().Contains(keyword));

        FilteredMedicines.Clear();
        foreach (var m in filteredMeds)
            FilteredMedicines.Add(m);

        FilteredOrders.Clear();
        foreach (var o in filteredOrds)
            FilteredOrders.Add(o);
    }
    }
}