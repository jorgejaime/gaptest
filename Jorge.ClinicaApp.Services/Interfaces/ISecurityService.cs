using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Messaging.Security;

namespace Jorge.ClinicaApp.Services.Interfaces
{
    public interface ISecurityService
    {

        ContractResponse<UserGetResponse> GetUser(ContractRequest<UserGetRequest> request);
        ContractResponse<UserGetResponse> ValidateUser(ContractRequest<LoginRequest> request);
        



    }
}
