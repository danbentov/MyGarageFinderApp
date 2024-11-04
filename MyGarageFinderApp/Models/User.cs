using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGarageFinderApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public int UserStatusId { get; set; }
        public int GarageLicense { get; set; }

        public User() { }
    }
}
