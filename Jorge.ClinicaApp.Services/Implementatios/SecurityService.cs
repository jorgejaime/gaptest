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
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Mapping;

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



        public ContractResponse<UserGetResponse> GetUser(ContractRequest<UserGetRequest> request)
        {

            User user;

            if (string.IsNullOrEmpty(request.Data.UserName))
            {
                user = _userRepository.First(u => u.Id == request.Data.Id);
            }
            else
            {
               user =  _userRepository.First(u => u.UserName.Equals(request.Data.UserName, StringComparison.InvariantCultureIgnoreCase));
            }
            return ContractUtil.CreateResponse(request, new UserGetResponse
            {
                User = user?.ToUserView()
            });

        }

        public ContractResponse<UserGetResponse> ValidateUser(ContractRequest<LoginRequest> request)
        {
         
           var user = _userRepository.First(u => u.UserName.Equals(request.Data.UserName, StringComparison.InvariantCultureIgnoreCase) 
                        && u.UserName.Equals(request.Data.Password, StringComparison.InvariantCultureIgnoreCase));
            
            return ContractUtil.CreateResponse(request, new UserGetResponse
            {
                User = user?.ToUserView()
            });



        }
    }
}
