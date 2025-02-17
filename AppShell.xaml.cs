using MedicineClient.ViewModels;
using MedicineClient.Views;
using Microsoft.Maui.Controls;
namespace MedicineClient
{
    public partial class AppShell : Shell
    {
        public AppShell(ShellViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
            Routes();
        }
        private void Routes()
        {
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("LogOut", typeof(LogOut));
            Routing.RegisterRoute("StatusPage", typeof(StatusPage));
            Routing.RegisterRoute("AdminPage", typeof(AdminPage));
            Routing.RegisterRoute("ListingPage", typeof(ListingPage));
        }
    }
}
