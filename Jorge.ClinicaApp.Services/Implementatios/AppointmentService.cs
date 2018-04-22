using System;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Infrastructure.UnitOfWork;
using Jorge.ClinicaApp.Model.Repositories;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Appointment;
using Microsoft.Extensions.Logging;
using Jorge.ClinicaApp.Services.Mapping;

namespace Jorge.ClinicaApp.Services.Implementatios
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ILogger<AppointmentService> _logger;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _uow;

        public AppointmentService(IAppointmentRepository appointmentRepository,
            IUnitOfWork uow, ILogger<AppointmentService> logger)
        {
            _appointmentRepository = appointmentRepository;
            _uow = uow;
            _logger = logger;
        }

        public ContractResponse<AppointmentGetResponse> GetAll(ContractRequest<BaseRequest> request)
        {
            ContractResponse<AppointmentGetResponse> response;
            try
            {
                var modelList = _appointmentRepository.GetAll();
                var modelListResponse = modelList.ToAppointmentGetResponseList();

                response = ContractUtil.CreateResponse(request, modelListResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);

                response = ContractUtil.CreateInvalidResponse<AppointmentGetResponse>(ex);
            }

            return response;
        }

        public ContractResponse<AppointmentGetResponse> Get(ContractRequest<AppointmentGetRequest> request)
        {
            ContractResponse<AppointmentGetResponse> response;
            try
            {
                var model = _appointmentRepository.FindBy(e => e.Id == request.Data.Id);
                var modelListResponse = model.ToAppointmentGetResponseList();

                response = ContractUtil.CreateResponse(request, modelListResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);
                response = ContractUtil.CreateInvalidResponse<AppointmentGetResponse>(ex);
            }

            return response;
        }

    }
}
