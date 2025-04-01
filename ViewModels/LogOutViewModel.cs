using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System;
using MedicineClient.Models;
using MedicineClient.Views;
using Microsoft.Extensions.DependencyInjection;

namespace MedicineClient.ViewModels
{
    public class LogOutViewModel : ViewModelBase
    {
        public ICommand LogoutCommand { get; }

        public LogOutViewModel(IServiceProvider serviceProvider)
        {
            LogoutCommand = new Command(async () => await ShowLogoutAlert());
            this.service = serviceProvider;
        }
        private IServiceProvider service;
        private async Task ShowLogoutAlert()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet(
                "Choose an option",
                "Cancel",
                null,
                "Log Out",
                "Exit App");

            if (action == "Log Out")
            {
                await Logout();
            }
            else if (action == "Exit App")
            {
                ExitApp();
            }
        }

        private async Task Logout()
        {
            var loginPage = service.GetRequiredService<LoginPage>();
            ((App)Application.Current).LoggedInUser=new AppUser();
            Application.Current.MainPage = new NavigationPage(loginPage);
        }

        private void ExitApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
