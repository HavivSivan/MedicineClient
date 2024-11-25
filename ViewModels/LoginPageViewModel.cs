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
using Microsoft.Extensions.DependencyInjection;
using MedicineClient.Services;
namespace MedicineClient.ViewModels;

public class LoginPageViewModel : ViewModelBase
{
    public Command LoginCommand { get; }
    public Command GoRegister { get; }
    public LoginPageViewModel(MedicineWebApi proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.proxy = proxy;
        LoginCommand = new Command(OnLogin);
        _Name = "";
        _password = "";
        InServerCall = false;
        errorMsg = "";
        GoRegister = new Command(OnGotoRegister);
    }
    public MedicineWebApi proxy { get; set; }
    public IServiceProvider serviceProvider { get; set; }
    
    private async void OnGotoRegister()
    {
        AppShell shell = serviceProvider.GetService<AppShell>();
        ((App)Application.Current).MainPage = shell;
        Shell.Current.FlyoutIsPresented = false; 
        Shell.Current.GoToAsync("RegisterPage"); 
    }
    private string errorMsg;
    public string ErrorMsg
    {
        get => errorMsg;
        set
        {
            if (errorMsg != value)
            {
                errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
            }
        }
    }
    
    public async void OnLogin()
    {
        //Choose the way you want to blobk the page while indicating a server call
        InServerCall = true;
        ErrorMsg = "";
        //Call the server to login
        LoginInfo loginInfo = new LoginInfo { Username = Name, Password = Password };
        AppUser? u = await this.proxy.LoginAsync(loginInfo);

        InServerCall = false;

        //Set the application logged in user to be whatever user returned (null or real user)
        ((App)Application.Current).LoggedInUser = u;
        if (u == null)
        {
            ErrorMsg = "Invalid Username or password";
        }
        else
        {
            ErrorMsg = "";
            //Navigate to the main page
            //AppShell shell = serviceProvider.GetService<AppShell>();
            //TasksViewModel tasksViewModel = serviceProvider.GetService<TasksViewModel>();
            //tasksViewModel.Refresh(); //Refresh data and user in the tasksview model as it is a singleton
            //((App)Application.Current).MainPage = shell;
            //Shell.Current.FlyoutIsPresented = false; //close the flyout
            //Shell.Current.GoToAsync("Tasks"); //Navigate to the Tasks tab page
        }
    }
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