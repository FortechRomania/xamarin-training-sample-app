using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Bindings;
using ObjCRuntime;
using TrainingPreparation.Services;
using TrainingPreparation.ViewModels;

namespace TrainingPreparation.iOS;

public partial class LoginViewController : UIViewController, LoginViewModel.INavigationService
{
    private LoginViewModel _viewModel;
    private List<Binding> _bindings = new List<Binding>();


    public LoginViewController (NativeHandle handle) : base (handle)
    {
    }

    public void NavigateToMoviesScreen()
    {
        PerformSegue("LoginToMoviesSegue", null);

        //var moviesViewController = Storyboard.InstantiateViewController(nameof(MoviesViewController)) as MoviesViewController;
        //PresentViewController(moviesViewController, true, null);
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        _viewModel = new LoginViewModel(new LoginService());
        _viewModel.NavigationService = this;

        SetBindings();
    }

    private void SetBindings()
    {
        _bindings.Add(this.SetBinding(() => _viewModel.EmailAddressPlaceholder, () => EmailTextField.Placeholder));
        _bindings.Add(this.SetBinding(() => _viewModel.PasswordPlaceholder, () => PasswordTextField.Placeholder));

        _bindings.Add(this.SetBinding(() => _viewModel.EmailAddress, () => EmailTextField.Text, BindingMode.TwoWay));
        _bindings.Add(this.SetBinding(() => _viewModel.Password, () => PasswordTextField.Text, BindingMode.TwoWay));

        _bindings.Add(this.SetBinding(() => _viewModel.LoginButtonTitle).WhenSourceChanges(() => LoginButton.SetTitle(_viewModel.LoginButtonTitle, UIControlState.Normal)));

        LoginButton.SetCommand(_viewModel.LoginCommand);

        _bindings.Add(this.SetBinding(() => _viewModel.ErrorMessage, () => ErrorLabel.Text));
    }
}