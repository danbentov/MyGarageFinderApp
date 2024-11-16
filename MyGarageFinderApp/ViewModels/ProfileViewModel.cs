using MyGarageFinderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagementApp.Services;

namespace MyGarageFinderApp.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {

        private MyGarageFinderWebAPIProxy proxy;
        public ProfileViewModel(MyGarageFinderWebAPIProxy p)
        {
            this.proxy = p;
            ShowPasswordCommand = new Command(OnShowPassword);
            LicenseNumber = ((App)Application.Current).LoggedInUser.LicenseNumber;
            Name = ((App)Application.Current).LoggedInUser.FirstName;
            LastName = ((App)Application.Current).LoggedInUser.LastName;
            Email = ((App)Application.Current).LoggedInUser.Email;
            Password = ((App)Application.Current).LoggedInUser.UserPassword;
            UpdateCommand = new Command(OnUpdate);
            CancelCommand = new Command(OnCancel);
            NameError = "Name is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
        }


        #region Name
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");

            }
        }

        private string nameError;
        public string NameError
        {
            get
            {
                return this.nameError;
            }
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");

            }
        }

        private bool showNameError;
        public bool ShowNameError
        {
            get
            {
                return this.showNameError;
            }
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");

            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion

        #region LastName 
        private string lastName;
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");

            }
        }

        private string lastNameError;
        public string LastNameError
        {
            get
            {
                return this.lastNameError;
            }
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");

            }
        }

        private bool showLastNameError;
        public bool ShowLastNameError
        {
            get
            {
                return this.showLastNameError;
            }
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");

            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = string.IsNullOrEmpty(LastName);
        }
        #endregion

        #region Email

        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");

            }
        }

        private string emailError;
        public string EmailError
        {
            get
            {
                return this.emailError;
            }
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");

            }
        }

        private bool showEmailError;
        public bool ShowEmailError
        {
            get
            {
                return this.showEmailError;
            }
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");

            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is not valid";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
            }
            else
            {
                EmailError = "Email is required";
            }
        }
        #endregion

        #region LicenseNumber

        private string licenseNumber;
        public string LicenseNumber
        {
            get
            {
                return this.licenseNumber;
            }
            set
            {
                licenseNumber = value;
                OnPropertyChanged("LicenseNumber");

            }
        }

        
        #endregion

        #region Password

        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");

            }
        }

        private string passwordError;
        public string PasswordError
        {
            get
            {
                return this.passwordError;
            }
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");

            }
        }

        private bool showPasswordError;
        public bool ShowPasswordError
        {
            get
            {
                return this.showPasswordError;
            }
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");

            }
        }

        private void ValidatePassword()
        {
            //Password must include characters and numbers and be longer than 4 characters
            if (string.IsNullOrEmpty(password) ||
                password.Length < 4 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
                this.ShowPasswordError = false;
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


        #endregion

        public Command UpdateCommand { get; }
        public Command CancelCommand { get; }

        public async void OnUpdate()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();

            if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError)
            {
                //Create a new AppUser object with the data from the registration form
                User newUser = new User
                {
                    FirstName = Name,
                    LastName = LastName,
                    Email = Email,
                    LicenseNumber = ((App)Application.Current).LoggedInUser.LicenseNumber,
                    UserStatusId = ((App)Application.Current).LoggedInUser.UserStatusId,
                    UserPassword = Password,
                    GarageLicense = ((App)Application.Current).LoggedInUser.GarageLicense
                };

                //Call the update method on the proxy to update the new user
                InServerCall = true;
                newUser = await proxy.updateUser(newUser);
                InServerCall = false;

                //If the update was successful, navigate to the login page
                if (newUser != null)
                {
                    InServerCall = false;
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Update failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Update", errorMsg, "ok");
                }
            }
        }

        //Define a method that will be called upon pressing the cancel button
        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }
    }
}
