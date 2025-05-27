using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicineClient.Services;
using MedicineClient.Models;
using System.Windows.Input;
using MedicineClient.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using MedicineClient.Views;
namespace MedicineClient.ViewModels;
public class ListingPageViewModel : ViewModelBase
{
    private readonly IServiceProvider serviceProvider;
    private readonly MedicineWebApi proxy;

    public ListingPageViewModel(MedicineWebApi proxy, IServiceProvider serviceProvider)
    {
        this.proxy = proxy;
        this.serviceProvider = serviceProvider;
        Listing = new ObservableCollection<Medicine>();
        OnRefreshCommand = new Command(async () => await Refresh());
        OnBarcodeCommand = new Command(async () => await OnBarcode());
        SelectMedicineCommand = new Command(async () => await OnOrder());
        Task.Run(async () => await LoadMedicines());
        IsRefreshing = false;
    }

    public async Task Initialize(string? result)
    {
        SearchText = result;
        await LoadMedicines();
        ApplyFilter();
    }



    private List<Medicine> allMedicines = new();
    public Medicine SelectedMedicine { get => selectedMedicine; set { selectedMedicine = value; OnPropertyChanged(); } }
    private Medicine selectedMedicine;
    private ObservableCollection<Medicine> listing;
    public ObservableCollection<Medicine> Listing
    {
        get => listing;
        set { listing = value; OnPropertyChanged(); }
    }

    private string searchText;
    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            OnPropertyChanged();
            ApplyFilter(); 
        }
    }

    public Command OnBarcodeCommand { get; }
    public Command OnRefreshCommand { get; }
    public Command SelectMedicineCommand { get; }
    private bool isRefreshing;
    public bool IsRefreshing
    {
        get => isRefreshing;
        set { isRefreshing = value; OnPropertyChanged(); }
    }

    private async Task LoadMedicines()
    {
        try
        {
            var medicines = await proxy.GetMedicineList();
            allMedicines = medicines.Where(m => m.Status != null && m.Status.Mstatus == "Approved").ToList();
            ApplyFilter(); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading medicines: {ex.Message}");
        }
    }

    private void ApplyFilter()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Listing = new ObservableCollection<Medicine>(allMedicines);
        }
        else
        {
            var filtered = allMedicines.Where(m => m.MedicineName != null && m.MedicineName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
            Listing = new ObservableCollection<Medicine>(filtered);
        }
    }

    private async Task Refresh()
    {
        try
        {
            IsRefreshing = true;
            await LoadMedicines();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Refresh error: {ex.Message}");
        }
        finally
        {
            IsRefreshing = false;
        }
    }
    private async Task OnBarcode()
    {
        await ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<BarcodePage>());
    }
    private async Task OnOrder()
    {
        if (SelectedMedicine!=null)
        {
            OrderPageViewModel vm = new OrderPageViewModel(proxy, serviceProvider);
            vm.Initialize(SelectedMedicine);
            OrderPage page = new OrderPage(vm);
            await ((App)Application.Current).MainPage.Navigation.PushAsync(page);

        }
    }

}
