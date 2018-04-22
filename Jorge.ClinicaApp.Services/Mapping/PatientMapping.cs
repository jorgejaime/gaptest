using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Services.Messaging.Patient;

namespace Jorge.ClinicaApp.Services.Mapping
{
    public static class PatientMapping
    {
        public static IEnumerable<PatientGetResponse> ToPatientGetResponseList(this IEnumerable<Patient> list)
        {
            return Mapper.Map<List<PatientGetResponse>>(list);
        }

        public static PatientGetResponse ToPatientGetResponse(this Patient model)
        {
            return Mapper.Map<PatientGetResponse>(model);
        }
    }
}
