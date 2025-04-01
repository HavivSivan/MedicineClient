using MedicineClient.Models;
using MedicineClient.ViewModels;
using MedicineClient.Views;
namespace MedicineClient
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        public AppUser LoggedInUser=new AppUser();
        public App(IServiceProvider serviceProvider)
        {
            LoggedInUser = new AppUser();
            ShellViewModel viewModel = new ShellViewModel();
            InitializeComponent();
            Services = serviceProvider;
            var loginPage = Services.GetService<LoginPage>();
            MainPage = new NavigationPage(loginPage);
        }
    }
}
