using System;
using System.Collections.Generic;

namespace Jorge.ClinicaApp.Model.DomainModels
{
    public partial class AppointmentType
    {
        public AppointmentType()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Appointment> Appointment { get; set; }

    }
}
