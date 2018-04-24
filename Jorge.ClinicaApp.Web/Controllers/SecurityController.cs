using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Jorge.ClinicaApp.Infrastructure.Messaging;
using Jorge.ClinicaApp.Services.Interfaces;
using Jorge.ClinicaApp.Services.Messaging.Security;
using Jorge.ClinicaApp.Services.ViewModels.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Jorge.ClinicaApp.Web.Services.Controllers
{
    [Route("[controller]/[action]")]
    public class SecurityController : Controller
    {
        private readonly ILogger<SecurityController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;

        public SecurityController(
            ILogger<SecurityController> logger,
            IConfiguration Configuration,
            ISecurityService securityService)
        {

            _logger = logger;
            _configuration = Configuration;
            _securityService = securityService;
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        private string GenerateToken(ContractRequest<LoginRequest> request)
        {

            var response = _securityService.ValidateUser(request);

            if (response.IsValid && response.Data.Any())
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Data.UserName),
                };

                foreach (var rol in response.Data[0].User.Roles)
                {

                    claims.Append(new Claim(ClaimTypes.Role, rol.RoleName));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "elbarcotechnology.com",
                    audience: "Jorge",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return "";
        }
    }
}