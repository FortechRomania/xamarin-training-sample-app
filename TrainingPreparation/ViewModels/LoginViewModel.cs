using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TrainingPreparation.Services;

namespace TrainingPreparation.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private ILoginService _loginService;

        private string _emailAddress;
        private string _password;
        private string _errorMessage;

        private bool _connectionInProgress;

        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            LoginCommand = new RelayCommand(
                async ()=> await LoginAsync(), 
                () => !string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(Password) && !ConnectionInProgress);
        }

        public interface INavigationService
        {
            void NavigateToMoviesScreen();
        }

        public INavigationService NavigationService { get; set; }

        public string EmailAddressPlaceholder => "Email Address";

        public string PasswordPlaceholder => "Password";

        public string EmailAddress 
        {
            get => _emailAddress;
            set
            {
                Set(ref _emailAddress, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password 
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string LoginButtonTitle => "Log in";

        public string ErrorMessage 
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }

        public RelayCommand LoginCommand { get; set; }

        public bool ConnectionInProgress
        {
            get => _connectionInProgress;
            set 
            {
                Set(ref _connectionInProgress, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private async Task LoginAsync()
        {
            try 
            {
                ConnectionInProgress = true;
                await _loginService.LoginAsync(EmailAddress, Password);
                NavigationService?.NavigateToMoviesScreen();
            }
            catch(Exception e)
            {
                ErrorMessage = e.Message;
            }
            finally
            {
                ConnectionInProgress = false;
            }
        }

    }
}
