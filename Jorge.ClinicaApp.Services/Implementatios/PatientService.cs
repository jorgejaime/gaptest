using System;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Infrastructure.UnitOfWork;
using Jorge.ClinicaApp.Model.Repositories;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Patient;
using Microsoft.Extensions.Logging;
using Jorge.ClinicaApp.Services.Mapping;


namespace Jorge.ClinicaApp.Services.Implementatios
{
    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _uow;

        public PatientService(IPatientRepository patientRepository,
            IUnitOfWork uow, ILogger<PatientService> logger)
        {
            _patientRepository = patientRepository;
            _uow = uow;
            _logger = logger;
        }

        public ContractResponse<PatientGetResponse> GetAll(ContractRequest<BaseRequest> request)
        {
            ContractResponse<PatientGetResponse> response;
            try
            {
                var modelList = _patientRepository.GetAll();
                var modelListResponse = modelList.ToPatientGetResponseList();

                response = ContractUtil.CreateResponse(request, modelListResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);

                response = ContractUtil.CreateInvalidResponse<PatientGetResponse>(ex);
            }

            return response;
        }

        public ContractResponse<PatientGetResponse> Get(ContractRequest<PatientGetRequest> request)
        {
            ContractResponse<PatientGetResponse> response;
            try
            {
                var model = _patientRepository.FindBy(e => e.Id == request.Data.Id);
                var modelListResponse = model.ToPatientGetResponseList();

                response = ContractUtil.CreateResponse(request, modelListResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);
                response = ContractUtil.CreateInvalidResponse<PatientGetResponse>(ex);
            }

            return response;
        }

    }
}
