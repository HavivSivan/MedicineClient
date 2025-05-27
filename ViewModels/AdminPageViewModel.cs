
using MedicineClient.Models;
using MedicineClient.Services;
using MedicineClient.ViewModels;
using MedicineClient.Views;
namespace MedicineClient.ViewModels
{


    public class AdminPageViewModel : ViewModelBase
    {
        private MedicineWebApi WebService;
        private string searchUsername;
        private AppUser selectedUser;
        private string statusMessage;
        private bool finishedsearch;
        public string SearchUsername
        {
            get => searchUsername;
            set { searchUsername = value; OnPropertyChanged(); }
        }
        public bool Finishedsearch
        {
            get => finishedsearch;
            set { finishedsearch = value; OnPropertyChanged(); }
        }

        public AppUser SelectedUser
        {
            get => selectedUser;
            set { selectedUser = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => statusMessage;
            set { statusMessage = value; OnPropertyChanged(); }
        }

        public Command SearchCommand { get; }
        public Command EnableCommand { get; }
        public Command AddPharmacyCommand { get; }
        public IServiceProvider service;

        public AdminPageViewModel(MedicineWebApi WebService, IServiceProvider serviceProvider)
        {
            Finishedsearch = false;
            SelectedUser=new AppUser();
            this.WebService = WebService;
            this.service = serviceProvider;
            SearchCommand = new Command(async () => await SearchUser());
            EnableCommand = new Command(async () => await EnableUserAsync(), () => SelectedUser != null);
            AddPharmacyCommand = new Command(OnAddPharmacy);
        }
        public async void OnAddPharmacy()
        {
           await ((App)Application.Current).MainPage.Navigation.PushAsync(service.GetService<AddPharmacy>());
        }
        private async Task SearchUser()
        {
           
            SelectedUser = await WebService.GetUserByUsernameAsync(SearchUsername);
            StatusMessage = SelectedUser == null ? "User not found." : string.Empty;
            Finishedsearch=true;

        }

        private async Task EnableUserAsync()
        {
            if (SelectedUser.Rank == 1)
            {
                StatusMessage = "Admins cannot be deleted.";
                return;
            }

            bool success = await WebService.EnableUserAsync(SelectedUser.Id);
            StatusMessage = success ? "User disabled/enabled successfully." : "Fail";
            selectedUser.Active=!selectedUser.Active;
            
        }


    }
}
