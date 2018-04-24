using Jorge.ClinicaApp.Services.VieModels.Appointment;
using Jorge.ClinicaApp.Services.VieModels.Patient;

namespace Jorge.ClinicaApp.Services.Messaging.Appointment
{
    public class AddUpdateAppointmentRequest
    {
        public AppointmentView  Appointment { get; set; }
    }
}