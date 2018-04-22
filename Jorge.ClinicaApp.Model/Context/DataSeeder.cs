using Jorge.ClinicaApp.Model.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jorge.ClinicaApp.Model.Context
{
    public class DataSeeder
    {
        public static void SeedCountries(ClinicaContext context)
        {
            if (!context.AppointmentType.Any())
            {
                var appointmentTypes = new List<AppointmentType>
                {
                    new AppointmentType { Description = "Medicina General" },
                    new AppointmentType { Description = "Odontología" },
                    new AppointmentType { Description = "Pediatría" },
                    new AppointmentType { Description = "Neurología" },

                };

                context.AddRange(appointmentTypes);
                context.SaveChanges();
            }
        }


    }
}
