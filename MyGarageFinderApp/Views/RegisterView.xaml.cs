using MyGarageFinderApp.ViewModels;

namespace MyGarageFinderApp.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}