using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Jorge.ClinicaApp.Infrastructure.Encryption;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Security;
using Jorge.ClinicaApp.Services.ViewModels.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Jorge.ClinicaApp.Web.Services.Security
{
    internal class ApplicationUserStore : 
        IUserStore<ApplicationUser>,
        IUserRoleStore<ApplicationUser>
        ,IUserPasswordStore<ApplicationUser>
        ,IUserSecurityStampStore<ApplicationUser>

    {
        private readonly ISecurityService  _securityService;
        private readonly ILogger<ApplicationUserStore> _logger;

        public ApplicationUserStore(ILogger<ApplicationUserStore> logger, ISecurityService securityService) 
        {
             _logger = logger;
            _securityService = securityService;
        }

        public void Dispose()
        {

        }
       
        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var userResponse = _securityService.GetUser(new GetUserRequest { UserName  = user.UserName });
            return userResponse.User == null ? Task.FromResult<string>(null) : Task.FromResult (userResponse.User.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var userResponse = _securityService.GetUser(new GetUserRequest { Id = Convert.ToInt32(user.Id) });
            return userResponse.User == null ? Task.FromResult<string>(null) : Task.FromResult(userResponse.User.UserName);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var userResponse = _securityService.GetUser(new GetUserRequest { Id = Convert.ToInt32(userId) });
            if (userResponse.User == null)
                return Task.FromResult<ApplicationUser>(null);

            return Task.FromResult(UserToApplicationUser(userResponse.User));
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var userResponse = _securityService.GetUser(new GetUserRequest { UserName = normalizedUserName }); 
            if (userResponse.User == null)
                return Task.FromResult<ApplicationUser>(null);

            return Task.FromResult(UserToApplicationUser(userResponse.User));
        }

       

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var passHasher = new PasswordHasher<ApplicationUser>();
            var password = Encryption.DecryptTripleDes(user.PasswordHash);
            password = passHasher.HashPassword(user,password);
            return Task.FromResult(password); ;
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            IList<string> roles = new List<string> { "admin" };
            return Task.FromResult(roles);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        private static ApplicationUser UserToApplicationUser(UserView user)
        {
           return new ApplicationUser
            {
                UserName = user.UserName,
                Id = user.Id.ToString(),
                Email = user.UserName,
                PasswordHash = user.Password,
                IdCuenta = user.AccountId,
                SecurityStamp = user.UserName + user.Password

            };
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }
    }
}