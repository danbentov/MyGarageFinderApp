using MyGarageFinderApp.ViewModels;

namespace MyGarageFinderApp.Views;

public partial class RepairHistoryView : ContentPage
{
	public RepairHistoryView(RepairHistoryViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}