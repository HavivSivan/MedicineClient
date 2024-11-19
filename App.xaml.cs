using MedicineClient.Models;

namespace MedicineClient
{
    public partial class App : Application
    {
        public AppUser LoggedInUser;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            LoggedInUser = new AppUser();
        }
    }
}
