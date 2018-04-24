using System;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Infrastructure.UnitOfWork;
using Jorge.ClinicaApp.Model.Repositories;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Patient;
using Microsoft.Extensions.Logging;
using Jorge.ClinicaApp.Services.Mapping;
using System.Linq;

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

        public ContractResponse<PatientListGetResponse> GetAll(ContractRequest<BaseRequest> request)
        {
            ContractResponse<PatientListGetResponse> response;
            try
            {
                var modelList = _patientRepository.GetAll();
                var modelListResponse = modelList.ToPatientViewList();

                response = ContractUtil.CreateResponse(request, new PatientListGetResponse { Patients = modelListResponse.ToList() });
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);

                response = ContractUtil.CreateInvalidResponse<PatientListGetResponse>(ex);
            }

            return response;
        }

        public ContractResponse<PatientListGetResponse> Get(ContractRequest<PatientGetRequest> request)
        {
            ContractResponse<PatientListGetResponse> response;
            try
            {
                var model = _patientRepository.FindBy(e => e.Id == request.Data.Id);
                var modelListResponse = model.ToPatientViewList();

                response = ContractUtil.CreateResponse(request, new PatientListGetResponse { Patients = modelListResponse.ToList() });
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);
                response = ContractUtil.CreateInvalidResponse<PatientListGetResponse>(ex);
            }

            return response;
        }

        public ContractResponse<PatientGetResponse> Add(ContractRequest<AddUpdatePatientRequest> request)
        {
            try
            {

                var model = request.Data.Patient.ToPatient();
                var brokenRules = model.GetBrokenRules().ToList();

                if (!brokenRules.Any())
                {

                    _patientRepository.Add(model);
                    _uow.Commit();


                    var responseModel = new PatientGetResponse { Id = model.Id };
                    return ContractUtil.CreateResponse(request, responseModel);
                }

                return ContractUtil.CreateInvalidResponse<PatientGetResponse>(brokenRules);
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);
                return ContractUtil.CreateInvalidResponse<PatientGetResponse>(ex);
            }
        }

        public ContractResponse<PatientGetResponse> Update(ContractRequest<AddUpdatePatientRequest> request)
        {
            try
            {
                var model = request.Data.Patient.ToPatient();

                var brokenRules = model.GetBrokenRules().ToList();
                if (!brokenRules.Any())
                {
                    _patientRepository.Edit(model);
                    _uow.Commit();

                    var responseModel = new PatientGetResponse { Id = model.Id };
                    return ContractUtil.CreateResponse(request, responseModel);
                }


                return ContractUtil.CreateInvalidResponse<PatientGetResponse>(brokenRules);
            }
            catch (Exception ex)
            {
                _logger.LogError(20, ex, ex.Message);
                return ContractUtil.CreateInvalidResponse<PatientGetResponse>(ex);
            }
        }
    }
}
