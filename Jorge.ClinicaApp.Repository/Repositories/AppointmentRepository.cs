using System;
using System.Linq;
using Jorge.ClinicaApp.Model.Context;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Model.Repositories;

namespace Jorge.ClinicaApp.Repository.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ClinicaContext context) : base(context)
        {

        }

        public bool IsAppointmentInSameDay(Appointment entity)
        {
            return Entities.Any(a=> a.PatientId == entity.PatientId && a.AppointmentDate.Date == entity.AppointmentDate.Date  );
        }

    }
}
