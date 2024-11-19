using MyGarageFinderApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TasksManagementApp.Services;

namespace MyGarageFinderApp.ViewModels
{
    public class MyCarsProfileViewModel : ViewModelBase
    {

        private ObservableCollection<Vehicle> myVehicles;
        public ObservableCollection<Vehicle> MyVehicles
        {
            get
            {
                return this.myVehicles;
            }
            set
            {
                this.myVehicles = value;
                OnPropertyChanged("MyVehicles");
            }
        }

        private MyGarageFinderWebAPIProxy proxy;
        public string Name { get; set; }
        public string License { get; set; }
        public MyCarsProfileViewModel(MyGarageFinderWebAPIProxy p)
        {
            this.proxy = p;
            IsRefreshing = false;
            readVehicles();
            SingleSelectCommand = new Command(OnSingleSelectVehicle);
            AddCarCommand = new Command(OnAddCarCommand);
            UpdateUserCommand = new Command(OnUpdateCommand);
            Name = ((App)Application.Current).LoggedInUser.FirstName + " " + ((App)Application.Current).LoggedInUser.LastName;
            License = ((App)Application.Current).LoggedInUser.LicenseNumber;
        }

        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            readVehicles();

            IsRefreshing = false;
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.isRefreshing = value;
                OnPropertyChanged();
            }
        }
        #endregion


        public async void readVehicles()
        {
            this.MyVehicles = new ObservableCollection<Vehicle>();
            MyVehicles = await proxy.GetUserVehicles(((App)Application.Current).LoggedInUser);
        }


        private Vehicle selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get
            {
                return this.selectedVehicle;
            }
            set
            {
                this.selectedVehicle = value;
                OnPropertyChanged();
            }
        }

        public Command SingleSelectCommand { get; set; }
        public Command AddCarCommand { get; set; }
        public Command UpdateUserCommand { get; set; }


        async void OnSingleSelectVehicle()
        {
            if (SelectedVehicle != null)
            {
                var navParam = new Dictionary<string, object>()
            {
                { "selectedVehicle",SelectedVehicle}
            };
                //Add goto here to show details
                await Shell.Current.GoToAsync("repairHistoryView", navParam);

                SelectedVehicle = null;
            }
        }

        private async void OnAddCarCommand()
        {
            await Shell.Current.GoToAsync("addCarView");
        }

        private async void OnUpdateCommand()
        {
            await Shell.Current.GoToAsync("updateProfileView");
        }

    }
}
