using MedicineClient.Views;
using MedicineClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MedicineClient.Models;
using System.Runtime.CompilerServices;
namespace MedicineClient.ViewModels;

public class LoginPageViewModel : ViewModelBase
{

    private string Name;
    public string PName
    {
        get { return Name; }
        set
        {
            Name = value; OnPropertyChanged();

        }
    }
    private bool logged;
    public bool Logged { get { return logged; } set { logged = value; OnPropertyChanged(); } }
    private string password;
    public string Password
    {
        get { return password; }
        set
        {
            password = value; OnPropertyChanged();
        }
    }
    
}