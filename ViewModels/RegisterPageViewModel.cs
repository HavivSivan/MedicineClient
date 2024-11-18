using System.Diagnostics;
using System.Reflection;

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
        if(string.IsNullOrEmpty(value))
        {
            Errorname = $"{callerMethodName} Is required";
        }
        if (value.Length < 2 || value.Length > 16)
        {
            Errorname = $"{callerMethodName} must be between 3 and 16 characters";
            return false;
        }
        if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^[a-zA-Z]+$"))
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






