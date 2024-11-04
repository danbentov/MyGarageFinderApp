using MyGarageFinderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagementApp.Services;

namespace MyGarageFinderApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private MyGarageFinderWebAPIProxy myGarageFinderWebAPIProxy;

        public RegisterViewModel(MyGarageFinderWebAPIProxy p)
        {
            this.myGarageFinderWebAPIProxy = p;
            ShowPasswordCommand = new Command(OnShowPassword);
            RegisterCommand = new Command(OnRegister);
            CancelCommand = new Command(OnCancel);
            NameError = "Name is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            LicenseNumberError = "License Number is requierd";
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
                ValidateLicenseNumber();
                OnPropertyChanged("LicenseNumber");

            }
        }

        private string licenseNumberError;
        public string LicenseNumberError
        {
            get
            {
                return this.licenseNumberError;
            }
            set
            {
                licenseNumberError = value;
                OnPropertyChanged("LicenseNumberError");

            }
        }

        private bool showLicenseNumberError;
        public bool ShowLicenseNumberError
        {
            get
            {
                return this.showLicenseNumberError;
            }
            set
            {
                showLicenseNumberError = value;
                OnPropertyChanged("ShowLicenseNumberError");

            }
        }

        private void ValidateLicenseNumber()
        {
            this.ShowLicenseNumberError = string.IsNullOrEmpty(LicenseNumber);
            
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


        public Command RegisterCommand { get; }
        public Command CancelCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnRegister()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidateLicenseNumber();
            ValidatePassword();

            if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowLicenseNumberError)
            {
                //Create a new AppUser object with the data from the registration form
                var newUser = new User
                {
                    FirstName = Name,
                    LastName = LastName,
                    Email = Email,
                    LicenseNumber = LicenseNumber,
                    UserStatusId = 1,
                    UserPassword = Password,
                    GarageLicense = 0
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newUser = await myGarageFinderWebAPIProxy.Register(newUser);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newUser != null)
                {
                    InServerCall = false;
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
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
