using MyGarageFinderApp.Models;
using MyGarageFinderApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            readHistoryRepairs();
        }

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


        private object selectedVehicle;
        public object SelectedVehicle
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
