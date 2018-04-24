using Jorge.ClinicaApp.Infrastructure.Model;
using Jorge.ClinicaApp.Model.DomainModels;

namespace Jorge.ClinicaApp.Model.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
         bool IsAppointmentInSameDay(Appointment entity);
    }
}
