using System.Collections.Generic;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jorge.ClinicaApp.Web.Services.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/patients")]
    public class PatientsController : Controller
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var request = ContractUtil.CreateRequest(new BaseRequest());
            var response = _patientService.GetAll(request);

            return Json(response);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var patientsResponse = _patientService.Get(ContractUtil.CreateRequest(new PatientGetRequest { Id = id }));
            return Json(patientsResponse);
        }

       
        [HttpPost]
        public JsonResult Post([FromBody]ContractRequest<AddUpdatePatientRequest> request)
        {
            var patientsResponse = _patientService.Add(request);
            return Json(patientsResponse);
        }
        

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]ContractRequest<AddUpdatePatientRequest> request)
        {
            if (id != request.Data.Patient.Id)
                return BadRequest();

            var patientsResponse = _patientService.Add(request);
            return Json(patientsResponse);
        }
        
        
    }
}
