using System;
using System.Collections.Generic;
using System.Text;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Messaging.Appointment;
using Jorge.ClinicaApp.Services.Messaging.Patient;

namespace Jorge.ClinicaApp.Services.Interfaces
{
    public interface IPatientService
    {
        ContractResponse<PatientGetResponse> GetAll(ContractRequest<BaseRequest> request);
        ContractResponse<PatientGetResponse> Get(ContractRequest<PatientGetRequest> request);

    }
}
