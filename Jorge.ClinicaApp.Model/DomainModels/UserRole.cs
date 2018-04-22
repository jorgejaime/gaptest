using System;
using System.Collections.Generic;

namespace Jorge.ClinicaApp.Model.DomainModels
{ 
    public partial class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}
