using System;
using Jorge.ClinicaApp.Model.Context;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Model.Repositories;

namespace Jorge.ClinicaApp.Repository.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ClinicaContext context) : base(context)
        {

        }

    }
}
