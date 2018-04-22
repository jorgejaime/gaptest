using System;
using System.Collections.Generic;

namespace Jorge.ClinicaApp.Model.DomainModels
{
    public partial class Patient
    {
        public Patient()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Ege { get; set; }
        public string Gender { get; set; }

        public ICollection<Appointment> Appointment { get; set; }

    }
}
