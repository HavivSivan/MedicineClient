
using MedicineClient.Models;
using MedicineClient.Services;
using MedicineClient.ViewModels;
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
        public Command DeleteCommand { get; }

        public AdminPageViewModel(MedicineWebApi WebService)
        {
            Finishedsearch = false;
            SelectedUser=new AppUser();
            this.WebService = WebService;
            SearchCommand = new Command(async () => await SearchUser());
            DeleteCommand = new Command(async () => await DeleteUser(), () => SelectedUser != null);
        }

        private async Task SearchUser()
        {
           
            SelectedUser = await WebService.GetUserByUsernameAsync(SearchUsername);
            StatusMessage = SelectedUser == null ? "User not found." : string.Empty;
            Finishedsearch=true;

        }

        private async Task DeleteUser()
        {
            if (SelectedUser.Rank == 1)
            {
                StatusMessage = "Admins cannot be deleted.";
                return;
            }

            bool success = await WebService.DeleteUserAsync(SelectedUser!.Id);
            StatusMessage = success ? "User deleted successfully." : "Failed to delete user.";
            if (success) SelectedUser = null;
        }


    }
}
