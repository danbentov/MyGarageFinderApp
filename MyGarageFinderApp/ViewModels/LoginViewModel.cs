using Microsoft.Extensions.DependencyInjection;
using MyGarageFinderApp.Models;
using MyGarageFinderApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TasksManagementApp.Services;

namespace MyGarageFinderApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private MyGarageFinderWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public LoginViewModel(MyGarageFinderWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            ShowPasswordCommand = new Command(OnShowPassword);
            licenseNumber = "";
            password = "";
            InServerCall = false;
            errorMsg = "";
        }

        private string licenseNumber;
        public string LicenseNumber

        {
            get
            {
                return this.licenseNumber;
            }
            set
            {
                this.licenseNumber = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password

        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                OnPropertyChanged();
            }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get
            {
                return this.errorMsg;
            }
            set
            {
                errorMsg = value;
                OnPropertyChanged(nameof(ErrorMsg));
                
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall = true;
            ErrorMsg = "";
            //Call the server to login
            LoginInfo loginInfo = new LoginInfo { LicenseNumber = LicenseNumber, UserPassword = Password };
            User u = await this.proxy.LoginAsync(loginInfo);

            InServerCall = false;

            //Set the application loggedd in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                ErrorMsg = "Invalid email or password";
            }
            else
            {
                ErrorMsg = "";
                //Navigate to the main page
                AppShell shell = serviceProvider.GetService<AppShell>();
                GaragesHomePageViewModel garagesHomePageViewModel = serviceProvider.GetService<GaragesHomePageViewModel>();
               /* garagesHomePageViewModel.Refresh();*/ //Refresh  data and user in the tasksview model as it is a singleton
                ((App)Application.Current).MainPage = shell;
                Shell.Current.FlyoutIsPresented = false; //close the flyout
            }
        }

        private void OnRegister()
        {
            ErrorMsg = "";
            LicenseNumber = "";
            Password = "";
            // Navigate to the Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<RegisterView>());
        }


        private bool isPassword = true;
        public bool IsPassword
        {
            get
            {
                return this.isPassword;
            }
            set
            {
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }
        //This command will trigger on pressing the password eye icon
        public Command ShowPasswordCommand { get; }
        //This method will be called when the password eye icon is pressed
        public void OnShowPassword()
        {
            //Toggle the password visibility
            IsPassword = !IsPassword;
        }

    }
}
