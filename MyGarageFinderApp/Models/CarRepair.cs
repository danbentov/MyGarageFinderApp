using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGarageFinderApp.Models
{
    public class CarRepair
    {
        public int RepairID { get; set; }
        public string LicensePlate { get; set; }
        public int? GarageID { get; set; }
        public DateTime? RepairDate { get; set; }
        public string DescriptionCar { get; set; }
        public int? Cost { get; set; }
        public string GarageName { get; set; }
    }
}
