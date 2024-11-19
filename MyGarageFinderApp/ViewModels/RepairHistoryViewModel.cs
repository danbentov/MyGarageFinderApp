using MyGarageFinderApp.Models;
using MyGarageFinderApp.Services;
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

    [QueryProperty(nameof(SelectedVehicle), "selectedVehicle")]
    public class RepairHistoryViewModel : ViewModelBase
    {
        private MyGarageFinderWebAPIProxy myGarageFinderWebAPIProxy;
        private TheGarageManagerWebAPIProxy theGarageManagerWebAPIProxy;
        public RepairHistoryViewModel(MyGarageFinderWebAPIProxy p, TheGarageManagerWebAPIProxy p2)
        {
            this.myGarageFinderWebAPIProxy = p;
            this.theGarageManagerWebAPIProxy = p2;
            IsRefreshing = false;
            readHistoryRepairs();
        }

        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            readHistoryRepairs();

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

        public async void readHistoryRepairs()
        {
            this.SelectedCarRepairs = new ObservableCollection<CarRepair>();
            VehicleLicense l = new VehicleLicense();
            if (SelectedVehicle is Vehicle)
            {
                Vehicle vehicle = (Vehicle)SelectedVehicle;
                l.LicensePlate = vehicle.LicensePlate;
            }
            SelectedCarRepairs = await theGarageManagerWebAPIProxy.GetVehicleRepairs(l);
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
                OnPropertyChanged(nameof(SelectedVehicle));
            }
        }

        private ObservableCollection<CarRepair> selectedCarRepairs;
        public ObservableCollection<CarRepair> SelectedCarRepairs
        {
            get
            {
                return this.selectedCarRepairs;
            }
            set
            {
                this.selectedCarRepairs = value;
                OnPropertyChanged();
            }
        }

    }
}
