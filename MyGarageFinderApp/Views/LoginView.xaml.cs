using MyGarageFinderApp.ViewModels;

namespace MyGarageFinderApp.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}