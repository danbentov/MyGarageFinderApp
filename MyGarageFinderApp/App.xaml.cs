using MyGarageFinderApp.Models;

namespace MyGarageFinderApp
{
    public partial class App : Application
    {
        public User? LoggedInUser { get; set; }
        public App()
        {
            InitializeComponent();
            LoggedInUser = null;
            MainPage = new AppShell();
        }
    }
}
