using System;
using System.Collections.Generic;
using System.Text;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Messaging.Appointment;

namespace Jorge.ClinicaApp.Services.Interfaces
{
    public interface IAppointmentService
    {
        ContractResponse<AppointmentListGetResponse> GetAll(ContractRequest<BaseRequest> request);
        ContractResponse<AppointmentListGetResponse> Get(ContractRequest<AppointmentGetRequest> request);
        ContractResponse<AppointmentGetResponse> Add(ContractRequest<AddUpdateAppointmentRequest> request);
        ContractResponse<AppointmentGetResponse> Update(ContractRequest<AddUpdateAppointmentRequest> request);

    }
}
