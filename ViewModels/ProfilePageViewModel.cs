using System.Text.RegularExpressions;
using System.Windows.Input;
using MedicineClient.Models;
using MedicineClient.Services;


namespace MedicineClient.ViewModels
{
    public partial class ProfilePageViewModel : ViewModelBase
    {
        private readonly MedicineWebApi proxy;
        private AppUser currentUser;
        public AppUser CurrentUser
        {  get { return currentUser; } set { currentUser=value; OnPropertyChanged(); } }

        private string newUsername;
        public string NewUsername
        { get { return newUsername; } set { newUsername = value; OnPropertyChanged(); } }

        private string newPassword;
        public string NewPassword
        { get { return newPassword; } set { newPassword=value; OnPropertyChanged(); } }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; OnPropertyChanged(); }
        }


        private bool hasError;
        public bool HasError
        {
            get { return hasError; }
            set { hasError = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public ProfilePageViewModel(MedicineWebApi proxy)
        {
            this.proxy = proxy;
            SaveCommand = new Command(async () => await SaveChangesAsync());

            currentUser = ((App)Application.Current).LoggedInUser;
        }

        private async Task SaveChangesAsync()
        {
            HasError = false;
            ErrorMessage = "";

            if (string.IsNullOrWhiteSpace(NewUsername))
            {
                SetError("Username cannot be empty.");
                return;
            }

            if (!ValidatePassword(NewPassword))
            {
                SetError("Password must be longer than 4 characters and contain at least one letter and one number.");
                return;
            }

            bool isTaken = await proxy.IsUsernameTakenAsync(NewUsername);
            if (isTaken && NewUsername != currentUser.UserName)
            {
                SetError("Username is already taken.");
                return;
            }

            currentUser.UserName = NewUsername;
            currentUser.UserPassword = NewPassword;

            bool success = await proxy.UpdateUserAsync(currentUser);
            if (!success)
            {
                SetError("Failed to update user. Try again.");
                return;
            }

            await Shell.Current.DisplayAlert("Success", "Profile updated successfully!", "OK");
        }

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length <= 4)
                return false;

            return true;
        }

        private void SetError(string message)
        {
            ErrorMessage = message;
            HasError = true;
        }
    }
}
