using MedicineClient.Models;
using MedicineClient.ViewModels;

namespace MedicineClient
{
    public partial class App : Application
    {
        public AppUser LoggedInUser;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell(vm);
            LoggedInUser = new AppUser();
        }
        ShellViewModel vm = new ShellViewModel();

    }
}
