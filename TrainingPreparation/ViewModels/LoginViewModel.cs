using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrainingPreparation.Services;

namespace TrainingPreparation.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private ILoginService _loginService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _emailAddress;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _password;

    [ObservableProperty]
    private string _errorMessage;

    public LoginViewModel(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public interface INavigationService
    {
        void NavigateToMoviesScreen();
    }

    public INavigationService NavigationService { get; set; }

    public string EmailAddressPlaceholder => "Email Address";

    public string PasswordPlaceholder => "Password";

    public string LoginButtonTitle => "Log in";

    [RelayCommand(CanExecute = nameof(CanLogIn))]
    private async Task LoginAsync()
    {
        try 
        {
            await _loginService.LoginAsync(EmailAddress, Password);

            NavigationService?.NavigateToMoviesScreen();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    private bool CanLogIn() => !string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(Password);
}
