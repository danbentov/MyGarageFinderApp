using MyGarageFinderApp.ViewModels;

namespace MyGarageFinderApp.Views;

public partial class AddCarView : ContentPage
{
	public AddCarView(AddCarViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}