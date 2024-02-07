using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI_demo1.Models;

namespace webAPI_demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly MylocalDatabaseContext context;

        public EmployeeController(MylocalDatabaseContext context)
        {
            this.context = context;
        }


        [Authorize]
        [HttpGet]
        [Route("getdata")]
        public string GetData()
        {
            return "Authenticated with JWt";
        }

        [Authorize]
        [HttpGet]
        [Route("details")]
        public string Details()
        {
            return "Authenticated with JWt";
        }

        [Authorize]
        [HttpPost]
        [Route("adduser")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                await context.User.AddAsync(user);
                await context.SaveChangesAsync();
                return Ok(new { UserName = user.UserName });
            }
            return BadRequest(ModelState);
        }
    }
}
