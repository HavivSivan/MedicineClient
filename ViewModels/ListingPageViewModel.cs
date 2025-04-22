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
    private readonly MedicineWebApi proxy;
    private readonly DrugWebAPI DrugProxy;

    Command OnBarcode { get; }
    public ListingPageViewModel(MedicineWebApi proxy, ServiceProvider serviceProvider)
    {
        this.proxy = proxy;
        Listing = new ObservableCollection<Medicine>();
        OnRefresh = new Command(async () => await Refresh());
        OnBarcode = new Command(async () => await ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<BarcodePage>()));
        IsRefreshing = false;
        SearchText = string.Empty;

        Task.Run(async () => await LoadMedicines());
    }

    private List<Medicine> allMedicines = new();
    private string detectedCode;
    public string DetectedCode
    {
        get => detectedCode;
        set { detectedCode = value; OnPropertyChanged(); }
    }
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

    public Command OnRefresh { get; }

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
            allMedicines = medicines
                .Where(m => m.Status != null && m.Status.Mstatus == "Approved")
                .ToList();

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
            var filtered = allMedicines
                .Where(m => m.MedicineName != null &&
                            m.MedicineName.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

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


}
