using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Services.Messaging;
using Jorge.ClinicaApp.Services.Messaging.Appointment;
using Jorge.ClinicaApp.Services.VieModels.Patient;
using Jorge.ClinicaApp.Services.ViewModels.Security;

namespace Jorge.ClinicaApp.Services
{
    public class AutoMapperBootStrapper
    {
        public static void ConfigureAutoMapper()
        {
           
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Appointment, AppointmentView>();
                cfg.CreateMap<AppointmentView, Appointment>();

                cfg.CreateMap<Patient, PatientView>();
                cfg.CreateMap<PatientView, Patient>();

                cfg.CreateMap<User, UserView>();
                cfg.CreateMap<UserView, User>();

            });
            
        }

    }
    }
