using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Services.VieModels.Appointment;
using Jorge.ClinicaApp.Services.VieModels.Patient;

namespace Jorge.ClinicaApp.Services.Mapping
{
    public static class AppointmentMapping
    {
        public static IEnumerable<AppointmentView> ToAppointmentViewList(this IEnumerable<Appointment> list)
        {
            return Mapper.Map<List<AppointmentView>>(list);
        }

        public static AppointmentView ToAppointmentView(this Appointment model)
        {
            return Mapper.Map<AppointmentView>(model);
        }


        public static IEnumerable<Appointment> ToAppointmentList(this IEnumerable<AppointmentView> list)
        {
            return Mapper.Map<List<Appointment>>(list);
        }

        public static Appointment ToAppointment(this AppointmentView model)
        {
            return Mapper.Map<Appointment>(model);
        }
    }
}
