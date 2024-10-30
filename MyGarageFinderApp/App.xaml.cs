using Microsoft.Extensions.DependencyInjection;
using MyGarageFinderApp.Models;
using MyGarageFinderApp.ViewModels;
using MyGarageFinderApp.Views;
using TasksManagementApp.Services;



namespace MyGarageFinderApp
{
    public partial class App : Application
    {
        public User? LoggedInUser { get; set; }
        private MyGarageFinderWebAPIProxy proxy;

        public App(IServiceProvider serviceProvider, MyGarageFinderWebAPIProxy proxy)
        {
            LoggedInUser = null;
            this.proxy = proxy;
            InitializeComponent();
            MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }
    }
}
