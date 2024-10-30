namespace MedicineClient.ViewModels;

public class RegisterPageViewModel : ViewModelBase
{
    private string _username;
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _password;

    public string Username
    {
        get => _username;
        set
        {
           
                _username = value;
            OnPropertyChanged();

        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            
            
                _firstName = value;
            OnPropertyChanged();

        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            
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

    public string Password
    {
        get => _password;
        set
        {
            
                _password = value;
            OnPropertyChanged();

        }
    }


}






