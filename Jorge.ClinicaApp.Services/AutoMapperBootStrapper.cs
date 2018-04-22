using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Services.Messaging;
using Jorge.ClinicaApp.Services.Messaging.Appointment;

namespace Jorge.ClinicaApp.Services
{
    public class AutoMapperBootStrapper
    {
        public static void ConfigureAutoMapper()
        {
           
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Appointment, AppointmentGetResponse>();
            });
            
        }

    }
    }
