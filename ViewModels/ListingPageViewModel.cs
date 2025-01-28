using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicineClient.Services;
using MedicineClient.Models;

namespace MedicineClient.ViewModels
{
    public class ListingPageViewModel : ViewModelBase
    {
        MedicineWebApi proxy;
        ServiceProvider serviceProvider;
        public ListingPageViewModel(MedicineWebApi proxy, ServiceProvider serviceProvider) 
        {
            this.proxy= proxy;
            this.serviceProvider= serviceProvider;
            temp = new List<Medicine>();
            GetMedicineList();
            Listing = new ObservableCollection<Medicine>(temp);
            OnRefresh = new Command(Refresh);
            IsRefreshing = false;
        }
        private ObservableCollection<Medicine> listing;
        public ObservableCollection<Medicine> Listing
        {
            get => listing; set { listing = value; OnPropertyChanged(); }
        }
        private List<Medicine> temp;
        private async void GetMedicineList()
        {
            temp=proxy.GetMedicineList().Result;
        }
        public Command OnRefresh;
        private async void Refresh()
        {
            if(!IsRefreshing)
            {
                IsRefreshing = true;
                GetMedicineList();
                IsRefreshing= false;
                Listing=new ObservableCollection<Medicine>(temp);
            }
        }
        public bool IsRefreshing;
    }
}
