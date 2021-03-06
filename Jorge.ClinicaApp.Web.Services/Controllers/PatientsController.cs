﻿using System.Collections.Generic;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jorge.ClinicaApp.Web.Services.Controllers
{
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
        public void Post([FromBody]string value)
        {
        }
        

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
