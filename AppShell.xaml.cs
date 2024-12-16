using MedicineClient.ViewModels;
using MedicineClient.Views;
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
            Routing.RegisterRoute("FirstPage", typeof(FirstPage));
        }
    }
}
