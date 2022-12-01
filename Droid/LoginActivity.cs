using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using AndroidX.Fragment.App;
using AndroidX.Lifecycle;
using CommunityToolkit.Mvvm.Bindings;
using TrainingPreparation.Services;
using TrainingPreparation.ViewModels;

namespace TrainingPreparation.Droid;

[Activity(Label = "LoginActivity", MainLauncher = true, Icon = "@mipmap/icon")]
public partial class LoginActivity : FragmentActivity, LoginViewModel.INavigationService
{
    private LoginViewModel _viewModel;
    private List<Binding> _bindings = new List<Binding>();

    public void NavigateToMoviesScreen()
    {
        StartActivity(typeof(MoviesActivity));
    }

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.Login);

        _viewModel = new ViewModelProvider(this).Get(() => new LoginViewModel(new LoginService()));
        _viewModel.NavigationService = this;

        SetBindings();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _viewModel.NavigationService = null;

        _bindings.ForEach(binding => binding.Detach());
        _bindings.Clear();
    }

    private void SetBindings()
    {
        _bindings.Add(this.SetBinding(() => _viewModel.EmailAddressPlaceholder, () => EmailEditText.Hint));
        _bindings.Add(this.SetBinding(() => _viewModel.PasswordPlaceholder, () => PasswordEditText.Hint));

        _bindings.Add(this.SetBinding(() => _viewModel.EmailAddress, () => EmailEditText.Text, BindingMode.TwoWay));
        _bindings.Add(this.SetBinding(() => _viewModel.Password, () => PasswordEditText.Text, BindingMode.TwoWay));

        _bindings.Add(this.SetBinding(() => _viewModel.LoginButtonTitle, () => LoginButton.Text));

        LoginButton.SetCommand(_viewModel.LoginCommand);

        _bindings.Add(this.SetBinding(() => _viewModel.ErrorMessage, () => ErrorTextView.Text));
    }
}
