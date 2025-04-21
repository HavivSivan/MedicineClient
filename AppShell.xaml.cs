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
            Items.Add(CreateTab("Listing", typeof(ListingPage)));
            Items.Add(CreateTab("Status", typeof(StatusPage)));
            Items.Add(CreateTab("Logout", typeof(LogOut)));


            if (currentUser.Rank == 1) 
                Items.Insert(0, CreateTab("Admin", typeof(AdminPage)));

            if (currentUser.Rank == 2)
                Items.Insert(0, CreateTab("Pharmacy", typeof(PharmacyPage)));
            else if (currentUser.Rank==3)
                Items.Insert(0, CreateTab("Profile", typeof(ProfilePage)));
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