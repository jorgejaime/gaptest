using System;

namespace Jorge.ClinicaApp.Services.VieModels.Patient
{
    public class AppointmentView
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentTypeId { get; set; }
    }
}