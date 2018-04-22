using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Services.Messaging.Appointment;

namespace Jorge.ClinicaApp.Services.Mapping
{
    public static class AppointmentMapping
    {
        public static IEnumerable<AppointmentGetResponse> ToAppointmentGetResponseList(this IEnumerable<Appointment> list)
        {
            return Mapper.Map<List<AppointmentGetResponse>>(list);
        }

        public static AppointmentGetResponse ToAppointmentGetResponse(this Appointment model)
        {
            return Mapper.Map<AppointmentGetResponse>(model);
        }
    }
}
