using System;
using System.Collections.Generic;

namespace Jorge.ClinicaApp.Model.DomainModels
{
    public partial class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}
