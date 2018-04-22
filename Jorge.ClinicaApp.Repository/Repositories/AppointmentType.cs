using System;
using Jorge.ClinicaApp.Model.Context;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Model.Repositories;

namespace Jorge.ClinicaApp.Repository.Repositories
{
    public class AppointmentTypeRepository : Repository<AppointmentType>, IAppointmentTypeRepository
    {
        public AppointmentTypeRepository(ClinicaContext context) : base(context)
        {

        }

    }
}
