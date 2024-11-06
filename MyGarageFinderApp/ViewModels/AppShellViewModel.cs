using MyGarageFinderApp.Models;
using MyGarageFinderApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGarageFinderApp.ViewModels
{
    public class AppShellViewModel : ViewModelBase 
    {
        private User currentUser;
        private IServiceProvider serviceProvider;
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.currentUser = ((App)Application.Current).LoggedInUser;
            IsManagingGarageMode = false;
            IsInUserMode = true;
            ManageGarageCommand = new Command(OnManageGarage);
        }

        public bool IsManager
        {
            get
            {
                if (currentUser.UserStatusId == 1)
                    return false;
                if (currentUser.UserStatusId == 2)
                    return true;
                return false;
            }
        }

        public bool IsGarageManager
        {
            get
            {
                if (currentUser.GarageLicense == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool IsManagingGarageMode { get; set; }
        public bool IsInUserMode { get; set; }



        //this command will be used for logout menu item
        public Command LogoutCommand
        {
            get
            {
                return new Command(OnLogout);
            }
        }
        //this method will be trigger upon Logout button click
        public void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;

            ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }

        public Command ManageGarageCommand { get; }
        public void OnManageGarage()
        {
            IsManagingGarageMode = !IsManagingGarageMode;
            IsInUserMode = !IsInUserMode;
        }

    }
}
