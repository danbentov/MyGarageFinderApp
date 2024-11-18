using MyGarageFinderApp.ViewModels;
using TasksManagementApp.Services;

namespace MyGarageFinderApp.Views;

public partial class MyCarsProfileView : ContentPage
{
	public MyCarsProfileView(MyCarsProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}