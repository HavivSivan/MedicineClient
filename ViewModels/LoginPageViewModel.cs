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
    public Command GoPharmacyLogin {  get; }
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
        ShowError = false;
        
    }
    
    private bool showError;
    public bool ShowError
    { get => showError; set { showError = value; OnPropertyChanged(nameof(ShowError)); } }
    public MedicineWebApi proxy { get; set; }
    public IServiceProvider serviceProvider { get; set; }
    
    private async void OnGotoRegister()
    {
        errorMsg = "";
        Name= "";
        Password = "";
        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<RegisterPage>());
        
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
        InServerCall = true;
        ErrorMsg = "";
        LoginInfo loginInfo = new LoginInfo { username = Name, password = Password };
        AppUser? u = await this.proxy.LoginAsync(loginInfo);
        InServerCall = false;
        ((App)Application.Current).LoggedInUser = u;
        if (u == null)
        {
            ShowError = true;
            ErrorMsg = "Invalid Username or password";
        }
        else
        {
            ShowError = false;
            ErrorMsg = "";
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<ListingPage>());

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