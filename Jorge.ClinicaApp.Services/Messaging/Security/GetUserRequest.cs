using System;
using System.Collections.Generic;
using System.Text;

namespace Jorge.ClinicaApp.Services.Messaging.Security
{
    public class GetUserRequest
    {
        public string UserName { get; set; }
        public int Id { get; set; }
    }
}
