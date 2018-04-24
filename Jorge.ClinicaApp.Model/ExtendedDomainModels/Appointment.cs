using Jorge.ClinicaApp.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jorge.ClinicaApp.Model.DomainModels
{
    public partial class Appointment : EntityBase
    {
     

        protected override void Validate()
        {
            if (AppointmentDate <  DateTime.Now.AddHours(24))
            {
                AddBrokenRule(new BusinessRule("AppointmentDate", "Las citas se deben agendar con mínimo 24 horas de antelación."));
            }

        }


    }
}
