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
        MedicineWebApi proxy;
        ServiceProvider serviceProvider;
        
        public ListingPageViewModel(MedicineWebApi proxy, ServiceProvider serviceProvider)
        {
            try
            {
                this.proxy = proxy;
                this.serviceProvider = serviceProvider;
                Listing = new ObservableCollection<Medicine>(temp);
                OnRefresh = new Command(Refresh);
                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private ObservableCollection<Medicine> listing;
        public ObservableCollection<Medicine> Listing
        {
            get => listing; set { listing = value; OnPropertyChanged(); }
        }
        static private List<Medicine> temp;
        private async void GetMedicineList()
        {
            temp = proxy.GetMedicineList().Result;
        }
        public Command OnRefresh { get; }
        private async void Refresh()
        {
            if (!IsRefreshing)
            {
                IsRefreshing = true;
                GetMedicineList();
                IsRefreshing = false;
                Listing = new ObservableCollection<Medicine>(temp);
            }
        }
        
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); }
        }
    }
}
