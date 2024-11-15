using MyGarageFinderApp.ViewModels;

namespace MyGarageFinderApp.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}