using System;
using System.Collections.Generic;

namespace Jorge.ClinicaApp.Model.DomainModels
{
    public partial class Appointment
    {
        public Appointment()
        {
   
        }

        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentTypeId { get; set; }
        public int PatientId { get; set; }

        public Patient Patient { get; set; }
        public AppointmentType AppointmentType { get; set; }

    }
}
