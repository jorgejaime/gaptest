using System;
using System.Collections.Generic;
using System.Text;
using Jorge.ClinicaApp.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Jorge.ClinicaApp.Model.DomainModels
{
    public partial class Patient : EntityBase
    {

      
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
