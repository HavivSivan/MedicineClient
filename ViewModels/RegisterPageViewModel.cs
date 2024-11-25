using MedicineClient.Services;
using System.Diagnostics;
using System.Reflection;
using MedicineClient.Models;
using Microsoft.Maui.Graphics.Text;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.DependencyInjection;
using MedicineClient.Views;
using MedicineClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;


namespace MedicineClient.ViewModels;

public class RegisterPageViewModel : ViewModelBase
{
    private string _username;
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _password;
    private string _errorname;
    private string _errorpass;
    private string _erroremail;
    public Command ClearCommand { get; }
    public Command RegisterCommand { get; }
    public Command RefreshCommand {  get; }
    public Command GotoLoginCommand {  get; }
    public RegisterPageViewModel(MedicineWebApi proxy, IServiceProvider serviceProvider)
    {
        this.proxy=proxy;
        this.serviceProvider=serviceProvider;
        RegisterCommand = new Command(OnRegister);
        RefreshCommand = new Command(OnRefresh);
        GotoLoginCommand = new Command(GotoLogin);
    }
    public MedicineWebApi proxy { get; set; }
    public IServiceProvider serviceProvider { get; set; }
    public async void GotoLogin()
    {
        AppShell shell = serviceProvider.GetService<AppShell>();
        ((App)Application.Current).MainPage = shell;
        Shell.Current.FlyoutIsPresented = false;
        Shell.Current.GoToAsync("Tasks"); 
    }
    public async void OnRefresh()
    {
        Username = new string("");
        FirstName = new string("");
        LastName = new string("");
        Email = new string("");
        Password = new string("");
        Errorname = new string("");
        Errorpass = new string("");
        Erroremail = new string("");
    }
    public bool ValidateAll()
    {
        return ValidateEmail(Email)&&ValidatePassword(Password)&&ValidateName(Username)&&ValidateName(FirstName)&&ValidateName(LastName);
    }
    public async void OnRegister()
    {
            if(ValidateAll())
        {
            var newUser = new AppUser
            {
                UserName = Username, FirstName = FirstName,
                LastName = LastName,
                UserEmail = Email,
                UserPassword = Password,
                Rank = 1
            };

            //Call the Register method on the proxy to register the new user
            InServerCall = true;
            newUser = await proxy.Register(newUser);
            InServerCall = false;

            //If the registration was successful, navigate to the login page
            if (newUser != null)
            {
                //UPload profile imae if needed
                
                InServerCall = false;

                ((App)(Application.Current)).MainPage.Navigation.PopAsync();
            }
            else
            {

                //If the registration failed, display an error message
                string errorMsg = "Registration failed. Please try again.";
                await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
            }
        }
    }
    public string Username
    {
        get => _username;
        set
        {
            if (!ValidateName(value)) return;
                _username = value;
            OnPropertyChanged();
           

        }
    }
    private bool ValidateName(string value)
    {
        MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
        string callerMethodName = caller.Name;
        callerMethodName = callerMethodName.TrimStart('s','e','t','_');
        if(string.IsNullOrEmpty(value))
        {
            Errorname = $"{callerMethodName} is required";
        }
        if (value.Length <= 2 || value.Length > 16)
        {
            Errorname = $"{callerMethodName} must be between 3 and 16 characters";
            return false;
        }
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[a-zA-Z]+$"))
        {
            Errorname = $"{callerMethodName} must be written in English characters";
            return false;
        }
        

        Errorname = "";
        return true;
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            
            if(!ValidateName(value)) return;
                _firstName = value;
            OnPropertyChanged();
           
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if(!ValidateName(value)) return;
                _lastName = value;
            OnPropertyChanged();

        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (!ValidateEmail(value))
                return;
                _email = value;
            OnPropertyChanged();

        }
    }
    private bool ValidateEmail(string value)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
        {
            Erroremail = "Email isn't valid";
            return false;
        }
        if(string.IsNullOrEmpty(value))
            {
            Erroremail = "Email is required.";
            return false; }
            Erroremail = "";
        return true;
    }
    private bool ValidatePassword(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Errorpass = "password is required";
            return false;
        }
        if(value.Length<8||value.Length>100)
        {
            Errorpass = "Password must be between 3 and 100 characters";
            return false;
        }
        if(!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[a-zA-Z0-9_]+$"))
        {
            Errorpass = "Password must be written in English letters, numbers and underscores.";
        }
        Errorpass = "";
        return true;
    }
    public string Password
    {
        get => _password;
        set
        {
            if (!ValidatePassword(value)) return;

                _password = value;
            OnPropertyChanged();

        }
    }
    public string Errorname
    {
        get => _errorname;
        set
        {
            _errorname = value;
            OnPropertyChanged();
        }
    }
    public string Errorpass
    {
        get => _errorpass;
        set
        {
            _errorpass = value;
            OnPropertyChanged();
        }
    }
    public string Erroremail
    {
        get => _erroremail;
        set { _errorpass = value;
        OnPropertyChanged();}
    }

}






