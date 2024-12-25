using MedicineClient.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MedicineClient.Views;

namespace MedicineClient.ViewModels
{
    public class LogOutViewModel : ViewModelBase
    {
        LogOutViewModel(IServiceProvider serviceProvider)
        {
            Logout = new Command(OnLogout);
            this.serviceProvider = serviceProvider;
        }
        IServiceProvider serviceProvider;

        Command Logout { get; }
        private async void OnLogout()
        {
            ((App)Application.Current).LoggedInUser  = new AppUser();
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<LoginPage>());
        }
    }
}
