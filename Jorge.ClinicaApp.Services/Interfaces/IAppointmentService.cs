using System;
using System.Collections.Generic;
using System.Text;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Messaging.Appointment;

namespace Jorge.ClinicaApp.Services.Interfaces
{
    public interface IAppointmentService
    {
        ContractResponse<AppointmentView> GetAll(ContractRequest<BaseRequest> request);
        ContractResponse<AppointmentView> Get(ContractRequest<AppointmentGetRequest> request);

    }
}
