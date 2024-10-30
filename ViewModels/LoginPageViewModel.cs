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

    private string _Name;
    public string Name
    {
        get { return _Name; }
        set
        {
            _Name = value; OnPropertyChanged();

        }
    }
    private bool _logged;
    public bool Logged { get { return _logged; } set { _logged = value; OnPropertyChanged(); } }
    private string _password;
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value; OnPropertyChanged();
        }
    }
    
}