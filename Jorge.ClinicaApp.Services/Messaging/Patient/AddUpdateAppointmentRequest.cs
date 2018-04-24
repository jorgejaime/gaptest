using Jorge.ClinicaApp.Services.VieModels.Patient;

namespace Jorge.ClinicaApp.Services.Messaging.Patient
{
    public class AddUpdatePatientRequest
    {
        public PatientView  Patient { get; set; }
    }
}