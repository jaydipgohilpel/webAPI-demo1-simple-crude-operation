using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webAPI_demo1.Migrations;
using webAPI_demo1.Models;

namespace webAPI_demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IConfiguration _config;
        private readonly MylocalDatabaseContext context;
        public UsersController(IConfiguration configuration, MylocalDatabaseContext context)
        {
            _config = configuration;
            this.context = context;
        }

        private User AuthenticateUser(User user)
        {
            /*     User _user = null;
                 if (user.UserName == "admin" && user.Password == "12@de")
                 {   
                     _user = new User { UserName ="meet vanpariya" };
                 }
                 return _user;*/
            try
            {

                User loginUser = context.User.FirstOrDefault(c => c.UserName == user.UserName && c.Password == user.Password);
                User _user = null;
                if (loginUser == null)
                {
                    return null;
                }

                _user = new User { UserName = loginUser.UserName };
                return _user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
         {
            new Claim(ClaimTypes.Name, user.UserName.Trim()),
            new Claim(ClaimTypes.Email, user.UserName.Trim()),
            // You can add more claims here if needed
        };


           var token = new JwtSecurityToken(
           issuer: _config["Jwt:Issuer"],
           audience: _config["Jwt:Audience"],
           claims: claims,
           expires: DateTime.Now.AddMinutes(1), // Set expiration time here
           signingCredentials: credentials
       );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult login(User user)
        {
            IActionResult responce = Unauthorized();
            var _user = AuthenticateUser(user);
            if (_user != null)
            {
                var token = GenerateToken(_user);
                responce = Ok(new { token = token });
            }
            return responce;

        }


    }
}
