using System.Collections.ObjectModel;
using MedicineClient.Models;
using MedicineClient.Services;
using MedicineClient.ViewModels;
using MedicineClient;
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
        LoadData();
    }

    private async void LoadData()
    {
        var currentUser = ((App)Application.Current).LoggedInUser;

        var allMedicines = await proxy.GetUserMedicinesAsync();
        var allOrders = await proxy.GetUserOrdersAsync();

        MyMedicines.Clear();
        MyOrders.Clear();

        foreach (var med in allMedicines.Where(m => m.user.Id == currentUser.Id))
            MyMedicines.Add(med);

        foreach (var ord in allOrders.Where(o => o.User.Id == currentUser.Id))
            MyOrders.Add(ord);

        FilterData();
    }

    public void FilterData()
    {
        string keyword = (SearchText ?? "").ToLower();

        var filteredMeds = MyMedicines.Where(m => m.MedicineName.ToLower().Contains(keyword) ||m.BrandName.ToLower().Contains(keyword)).ToList();

        var filteredOrds = MyOrders.Where(o => o.Medicine.MedicineName.ToLower().Contains(keyword) ||o.Sender.ToLower().Contains(keyword) ||o.Receiver.ToLower().Contains(keyword)).ToList();

        FilteredMedicines.Clear();
        foreach (var m in filteredMeds)
            FilteredMedicines.Add(m);

        FilteredOrders.Clear();
        foreach (var o in filteredOrds)
            FilteredOrders.Add(o);
    }
}
