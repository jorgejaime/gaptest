using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Jorge.ClinicaApp.Model.Repositories;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Infrastructure.UnitOfWork;
using Jorge.ClinicaApp.Services.Messaging.Security;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Services.ViewModels.Security;

namespace Jorge.ClinicaApp.Services.Implementatios
{


    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<SecurityService> _logger;
        private readonly IUnitOfWork _uow;

        public SecurityService(IUserRepository userRepository,
                             IUnitOfWork uow, ILogger<SecurityService> logger)
        {
            _userRepository = userRepository;
            _uow = uow;
            _logger = logger;
        }

        

        public GetUserResponse GetUser(GetUserRequest request)
        {

            User user;

            if (string.IsNullOrEmpty(request.UserName))
            {
                user = _userRepository.First(u => u.Id == request.Id);
            }
            else
            {
               user =  _userRepository.First(u => u.UserName.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase));
            }

            return new GetUserResponse
            {
                User = user  == null ? null : Mapper.Map<UserView>(user)
            };

        }
    }
}
