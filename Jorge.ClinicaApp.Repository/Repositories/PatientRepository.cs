using System;
using Jorge.ClinicaApp.Model.Context;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Model.Repositories;

namespace Jorge.ClinicaApp.Repository.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(ClinicaContext context) : base(context)
        {

        }

    }
}
