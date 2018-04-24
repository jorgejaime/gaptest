using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jorge.ClinicaApp.Web.Services.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        private string GenerateToken(string username)
        {

            var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    //new Claim(ClaimTypes.Actor, request.AccountId)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "elbarcotechnology.com",
                //audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);



            //var header = new JwtHeader(
            //    new SigningCredentials(
            //        new SymmetricSecurityKey(
            //            Encoding.UTF8.GetBytes(_configuration["SecurityKey"])
            //        ),
            //        SecurityAlgorithms.HmacSha256)
            //);

            //var claims = new Claim[]
            //{
            //    new Claim(JwtRegisteredClaimNames.UniqueName, username),
            //};
            //var payload = new JwtPayload(claims);

            //var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}