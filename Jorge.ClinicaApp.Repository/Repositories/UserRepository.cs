using System;
using Jorge.ClinicaApp.Model.Context;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Model.Repositories;

namespace Jorge.ClinicaApp.Repository.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ClinicaContext context) : base(context)   
        {

        }

    }
}
