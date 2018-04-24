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
        ContractResponse<PatientListGetResponse> GetAll(ContractRequest<BaseRequest> request);
        ContractResponse<PatientListGetResponse> Get(ContractRequest<PatientGetRequest> request);
        ContractResponse<PatientGetResponse> Add(ContractRequest<AddUpdatePatientRequest> request);
        ContractResponse<PatientGetResponse> Update(ContractRequest<AddUpdatePatientRequest> request);

    }
}
