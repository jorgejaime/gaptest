using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Services.VieModels.Patient;

namespace Jorge.ClinicaApp.Services.Mapping
{
    public static class PatientMapping
    {
        public static IEnumerable<PatientView> ToPatientViewList(this IEnumerable<Patient> list)
        {
            return Mapper.Map<List<PatientView>>(list);
        }

        public static PatientView ToPatientView(this Patient model)
        {
            return Mapper.Map<PatientView>(model);
        }


        public static IEnumerable<Patient> ToPatientList(this IEnumerable<PatientView> list)
        {
            return Mapper.Map<List<Patient>>(list);
        }

        public static Patient ToPatient(this PatientView model)
        {
            return Mapper.Map<Patient>(model);
        }
    }
}
