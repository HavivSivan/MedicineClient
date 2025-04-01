using MedicineClient.ViewModels;
using MedicineClient.Views;
using MedicineClient.Models;
namespace MedicineClient
{
    public partial class AppShell : Shell
    {
        public AppShell(AppUser currentUser)
        {
            InitializeComponent();
            // Common Pages
            Items.Add(CreateTab("Listing", typeof(ListingPage)));
            Items.Add(CreateTab("Status", typeof(StatusPage)));
            Items.Add(CreateTab("Logout", typeof(LogOut)));

            // Conditional Pages
            if (currentUser.Rank == 1) // Admin
                Items.Insert(0, CreateTab("Admin", typeof(AdminPage)));

            if (currentUser.Rank == 2) // Pharmacy
                Items.Insert(0, CreateTab("Pharmacy", typeof(PharmacyPage)));
        }

        private ShellContent CreateTab(string title, Type pageType)
        {
            return new ShellContent
            {
                Title = title,
                ContentTemplate = new DataTemplate(pageType)
            };
        }
    }
}