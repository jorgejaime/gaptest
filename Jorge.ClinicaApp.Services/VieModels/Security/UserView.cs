
using System.Collections.Generic;

namespace Jorge.ClinicaApp.Services.ViewModels.Security
{
    public class UserView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        public List<RoleView> Roles { get; set; }
   
    }
}
