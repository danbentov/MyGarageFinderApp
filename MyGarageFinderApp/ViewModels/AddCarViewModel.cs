using MyGarageFinderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagementApp.Services;
using static Java.Util.Jar.Attributes;

namespace MyGarageFinderApp.ViewModels
{
    public class AddCarViewModel : ViewModelBase
    {
        private MyGarageFinderWebAPIProxy proxy;
        public AddCarViewModel(MyGarageFinderWebAPIProxy proxy)
        {
            this.proxy = proxy;
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";
            showLicenseError = false;
            LicenseError = "License Plate should be at least 5 characters";
            ShowFieldsError = false;
            FieldsError = "Not all fields completed";
            IsSearched = false;
            notExist = true;
            FoundVehicle = "";
            SearchVehicleCommand = new Command(OnSearchVehicle);
            RegisterVehicleCommand = new Command(OnRegisterVehicle);
        }

        #region LicensePlate

        private string licensePlate;
        public string LicensePlate

        {
            get
            {
                return this.licensePlate;
            }
            set
            {
                this.licensePlate = value;
                validateLicense();
                IsSearched = false;
                notExist = true;
                OnPropertyChanged();
            }
        }

        private string licenseError;
        public string LicenseError
        {
            get
            {
                return this.licenseError;
            }
            set
            {
                licenseError = value;
                OnPropertyChanged("LicenseError");

            }
        }

        private bool showLicenseError;
        public bool ShowLicenseError
        {
            get
            {
                return this.showLicenseError;
            }
            set
            {
                showLicenseError = value;
                OnPropertyChanged("ShowLicenseError");

            }
        }

        private void validateLicense()
        {
            if (string.IsNullOrEmpty(LicensePlate) || LicensePlate.Length < 5)
                this.ShowLicenseError = true;
            else
            {
                this.showLicenseError = false;
            }
        }

        private string fieldsError;
        public string FieldsError
        {
            get
            {
                return this.fieldsError;
            }
            set
            {
                fieldsError = value;
                OnPropertyChanged("FieldsError");

            }
        }

        private bool showFieldsError;
        public bool ShowFieldsError
        {
            get
            {
                return this.showFieldsError;
            }
            set
            {
                showFieldsError = value;
                OnPropertyChanged("ShowFieldsError");

            }
        }

        private void validatefields()
        {
            ShowFieldsError = !(!string.IsNullOrEmpty(Manufactor) && !string.IsNullOrEmpty(Model) && !string.IsNullOrEmpty(Year) && !string.IsNullOrEmpty(Fuel) && !string.IsNullOrEmpty(Color));
        }

        #endregion

        private string manufactor;
        public string Manufactor

        {
            get
            {
                return this.manufactor;
            }
            set
            {
                this.manufactor = value;
                validatefields();
                OnPropertyChanged();
            }
        }


        private string model;
        public string Model

        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
                validatefields();
                OnPropertyChanged();
            }
        }

        private string year;
        public string Year

        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = value;
                validatefields();
                OnPropertyChanged();
            }
        }

        private string fuel;
        public string Fuel

        {
            get
            {
                return this.fuel;
            }
            set
            {
                this.fuel = value;
                validatefields();
                OnPropertyChanged();
            }
        }

        private string color;
        public string Color

        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                validatefields();
                OnPropertyChanged();
            }
        }

        #region Photo

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged("LocalPhotoPath");
            }
        }

        public Command UploadPhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnUploadPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void UpdatePhotoURL(string virtualPath)
        {
            Random r = new Random();
            PhotoURL = proxy.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
            LocalPhotoPath = "";
        }

        #endregion

        public Command SearchVehicleCommand { get; }
        public Command RegisterVehicleCommand { get; }

        public bool IsSearched { get; set; }
        public bool notExist { get; set; }

        private string foundVehicle;
        public string FoundVehicle

        {
            get
            {
                return this.foundVehicle;
            }
            set
            {
                this.foundVehicle = value;
                OnPropertyChanged();
            }
        }

        public async void OnSearchVehicle()
        {
            validateLicense();
            if (!showLicenseError)
            {
                VehicleLicense L = new VehicleLicense();
                L.LicensePlate = LicensePlate;
                InServerCall = true;
                 Vehicle? vehicle = await proxy.SearchVehicle(L);
                InServerCall = false;

                if (vehicle != null)
                {
                    Manufactor = vehicle.Manufacturer;
                    Model = vehicle.Model;
                    Year = vehicle.VehicleYear;
                    Fuel = vehicle.FuelType;
                    Color = vehicle.Color;
                    photoURL = vehicle.ImageURL;
                    FoundVehicle = "Vehicle is existed - you can register as an owner";
                    notExist = false;
                }
                else
                {
                    FoundVehicle = "Vehicle isnt existed - enter details";
                    notExist = true;
                }
                IsSearched = true;
            }
            else
            {
                string errorMsg = "search failed. Please try again.";
                await Application.Current.MainPage.DisplayAlert("Search", errorMsg, "ok");
            }
        }

        public async void OnRegisterVehicle()
        {
            validateLicense();
            validatefields();
            if (!showLicenseError && !string.IsNullOrEmpty(Manufactor) && !string.IsNullOrEmpty(Model) && !string.IsNullOrEmpty(Year) && !string.IsNullOrEmpty(Fuel) && !string.IsNullOrEmpty(Color))
            {
                Vehicle vhc = new Vehicle
                {
                    LicensePlate = LicensePlate,
                    Model = Model,
                    Manufacturer = manufactor,
                    VehicleYear = Year,
                    Color = Color,
                    FuelType = Fuel,
                    ImageURL = photoURL
                };
                VehicleUser v = new VehicleUser()
                {
                    user = ((App)Application.Current).LoggedInUser,
                    vehicle = vhc
                };
                InServerCall = true;
                VehicleUser? vehicleUser = await proxy.RegisterVehicle(v);
                InServerCall = false;

                if (vehicleUser != null)
                {
                    ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }
            else
            {
                
            }
        }
    }
}
 