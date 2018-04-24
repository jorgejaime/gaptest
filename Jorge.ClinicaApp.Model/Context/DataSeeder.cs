using Jorge.ClinicaApp.Infrastructure.Encryption;
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
        public static void Seed(ClinicaContext context)
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

            if (!context.User.Any())
            {
                var password = Encryption.EncryptTripleDes("12345678");
                context.User.Add(new User { UserName = "Admin", Password = password });
            }

            if (!context.Patient.Any())
            {
                var patients = new List<Patient>
                {
                    new Patient { Name = "Jorge", Ege = 36, Gender = "F"},
                     new Patient { Name = "Jaime", Ege = 36, Gender = "F"},

                };

                context.AddRange(patients);
                context.SaveChanges();
            }
        }
    }
}
