using Jorge.ClinicaApp.Services.Messaging.Security;

namespace Jorge.ClinicaApp.Services.Interfaces
{
    public interface ISecurityService
    {

        GetUserResponse GetUser(GetUserRequest request);
      

       
    }
}
