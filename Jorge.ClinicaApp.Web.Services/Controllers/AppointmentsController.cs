

using System.Collections.Generic;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Appointment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jorge.ClinicaApp.Web.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/appointments")]
    public class AppointmentsController : Controller
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IAppointmentService _appointmentService;
        public AppointmentsController(IAppointmentService appointmentService, ILogger<AppointmentsController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var request = ContractUtil.CreateRequest(new BaseRequest());
            var response = _appointmentService.GetAll(request);

            return Json(response);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var appointmentsResponse = _appointmentService.Get(ContractUtil.CreateRequest(new AppointmentGetRequest { Id = id }));
            return Json(appointmentsResponse);
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
