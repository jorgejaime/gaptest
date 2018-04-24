using Jorge.ClinicaApp.Services.VieModels.Patient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jorge.ClinicaApp.Services.Messaging.Patient
{
    public class PatientListGetResponse
    {
        public List<PatientView> Patients { get; set; }

    }
}
