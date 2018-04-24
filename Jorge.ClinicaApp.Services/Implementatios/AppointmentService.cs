using System;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Infrastructure.UnitOfWork;
using Jorge.ClinicaApp.Model.Repositories;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Appointment;
using Microsoft.Extensions.Logging;
using Jorge.ClinicaApp.Services.Mapping;
using Jorge.ClinicaApp.Model.DomainModels;
using AutoMapper;
using System.Linq;
using Jorge.ClinicaApp.Services.Mapping;
using Jorge.ClinicaApp.Infrastructure.Domain;
using System.Collections.Generic;

namespace Jorge.ClinicaApp.Services.Implementatios
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ILogger<AppointmentService> _logger;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _uow;

        public AppointmentService(IAppointmentRepository appointmentRepository,
            IUnitOfWork uow, ILogger<AppointmentService> logger, IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _uow = uow;
            _logger = logger;
            _patientRepository = patientRepository;
        }

        public ContractResponse<AppointmentListGetResponse> GetAll(ContractRequest<BaseRequest> request)
        {
            ContractResponse<AppointmentListGetResponse> response;
            try
            {
                var modelList = _appointmentRepository.GetAll();
                var modelListResponse = modelList.ToAppointmentViewList();

                response = ContractUtil.CreateResponse(request, new AppointmentListGetResponse { Appointments = modelListResponse.ToList() });
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);

                response = ContractUtil.CreateInvalidResponse<AppointmentListGetResponse>(ex);
            }

            return response;
        }

        public ContractResponse<AppointmentListGetResponse> Get(ContractRequest<AppointmentGetRequest> request)
        {
            ContractResponse<AppointmentListGetResponse> response;
            try
            {
                var model = _appointmentRepository.FindBy(e => e.Id == request.Data.Id);
                var modelListResponse = model.ToAppointmentViewList();

                response = ContractUtil.CreateResponse(request, new AppointmentListGetResponse { Appointments = modelListResponse.ToList() });
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);
                response = ContractUtil.CreateInvalidResponse<AppointmentListGetResponse>(ex);
            }

            return response;
        }

       

        public ContractResponse<AppointmentGetResponse> Add(ContractRequest<AddUpdateAppointmentRequest> request)
        {
            try
            {

                var model = request.Data.Appointment.ToAppointment();
                model.AppointmentInSameDate(_appointmentRepository.IsAppointmentInSameDay(model));

                var brokenRules = model.GetBrokenRules().ToList();
                if (!brokenRules.Any())
                {
                    

                    _appointmentRepository.Add(model);
                    _uow.Commit();


                    var responseModel = new AppointmentGetResponse { Id = model.Id };
                    return ContractUtil.CreateResponse(request, responseModel);
                }

                return ContractUtil.CreateInvalidResponse<AppointmentGetResponse>(brokenRules);
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);
                return ContractUtil.CreateInvalidResponse<AppointmentGetResponse>(ex);
            }
        }

        public ContractResponse<AppointmentGetResponse> Update(ContractRequest<AddUpdateAppointmentRequest> request)
            {
            try
            {
                var model = request.Data.Appointment.ToAppointment();
                model.AppointmentInSameDate(_appointmentRepository.IsAppointmentInSameDay(model));

                var brokenRules = model.GetBrokenRules().ToList();
                if (!brokenRules.Any())
                {
                    _appointmentRepository.Edit(model);
                    _uow.Commit();

                    var responseModel = new AppointmentGetResponse { Id = model.Id };
                    return ContractUtil.CreateResponse(request, responseModel);
                }


                return ContractUtil.CreateInvalidResponse<AppointmentGetResponse>(brokenRules);
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);
                return ContractUtil.CreateInvalidResponse<AppointmentGetResponse>(ex);
            }
        }


       
       

    }
}
