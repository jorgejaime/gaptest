using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Jorge.ClinicaApp.Web.Services.Security
{
    public class ApplicationUser : IdentityUser
    {
        public int IdCuenta { get; set; }
    }
}
