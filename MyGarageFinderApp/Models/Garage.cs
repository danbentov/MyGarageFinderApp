using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGarageFinderApp.Models
{
   
        public class Garage
    {
            public int GarageID { get; set; }
            public int? GarageNumber { get; set; }
            public string GarageName { get; set; }
            public string TypeCode { get; set; }
            public string GarageType { get; set; }
            public string GarageAddress { get; set; }
            public string City { get; set; }
            public string Phone { get; set; } // Changed to string
            public int? ZipCode { get; set; }
            public int? SpecializationCode { get; set; }
            public string Specialization { get; set; }
            public string GarageManager { get; set; }
            public int GarageLicense { get; set; }
            public DateOnly? TestTime { get; set; }
            public string WorkingHours { get; set; }

            public Garage() { }
        
    }
}
