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
namespace MedicineClient.ViewModels
{
    public class ListingPageViewModel : ViewModelBase
    {
        private readonly MedicineWebApi proxy;

        public ListingPageViewModel(MedicineWebApi proxy)
        {
            this.proxy = proxy;
            Listing = new ObservableCollection<Medicine>();
            OnRefresh = new Command(async () => await Refresh());
            IsRefreshing = false;

            _ = LoadMedicines(); 
        }

        private ObservableCollection<Medicine> listing;
        public ObservableCollection<Medicine> Listing
        {
            get => listing;
            set { listing = value; OnPropertyChanged(); }
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
                var allMedicines = await proxy.GetMedicineList();
                var approved = allMedicines.Where(m => m.Status.Mstatus=="Approved").ToList();
                Listing = new ObservableCollection<Medicine>(approved);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading medicines: {ex.Message}");
            }
        }

        private async Task Refresh()
        {
            if (!IsRefreshing)
            {
                IsRefreshing = true;
                await LoadMedicines();
                IsRefreshing = false;
            }
        }
    }

}