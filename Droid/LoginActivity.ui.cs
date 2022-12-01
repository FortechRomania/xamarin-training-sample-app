using Android.Widget;

namespace TrainingPreparation.Droid;

public partial class LoginActivity
{
    private EditText _emailEditText;
    private EditText _passwordEditText;
    private Button _loginButton;
    private TextView _errorTextView;

    public EditText EmailEditText => _emailEditText ?? (_emailEditText = FindViewById<EditText>(Resource.Id.emailAddressEditText));

    public EditText PasswordEditText => _passwordEditText ?? (_passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText));

    public Button LoginButton => _loginButton ?? (_loginButton = FindViewById<Button>(Resource.Id.loginButton));

    public TextView ErrorTextView => _errorTextView ?? (_errorTextView = FindViewById<TextView>(Resource.Id.errorTextView));
}
