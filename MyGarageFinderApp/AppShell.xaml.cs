using MyGarageFinderApp.ViewModels;
using MyGarageFinderApp.Views;

namespace MyGarageFinderApp
{
    public partial class AppShell : Shell
    {
     
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("garagesHomePageView", typeof(GaragesHomePageView));
        }
    }
}
