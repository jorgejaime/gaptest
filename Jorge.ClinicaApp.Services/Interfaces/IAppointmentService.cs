using System;
using System.Collections.Generic;
using System.Text;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Messaging.Appointment;

namespace Jorge.ClinicaApp.Services.Interfaces
{
    public interface IAppointmentService
    {
        ContractResponse<AppointmentGetResponse> GetAll(ContractRequest<BaseRequest> request);
        ContractResponse<AppointmentGetResponse> Get(ContractRequest<AppointmentGetRequest> request);

    }
}
