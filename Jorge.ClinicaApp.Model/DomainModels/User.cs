﻿using System;
using System.Collections.Generic;

namespace Jorge.ClinicaApp.Model.DomainModels
{
    public partial class User
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public int OrganizerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}