using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI_demo1.Models;

namespace webAPI_demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly MylocalDatabaseContext context;

        public StudentController(MylocalDatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var result = await context.Students.ToListAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentsById(int id)
        {
            var student=await context.Students.FindAsync(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> createStudent(Student std)
        {
            await context.Students.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> updateStudent(int id,Student std)
        {
            /*   if (id != std.Id)
                   return BadRequest();

               context.Entry(std).State = EntityState.Modified;
               await context.SaveChangesAsync();
               return Ok(std);*/

            if (id != std.Id)
            {
                return BadRequest();
            }

            context.Entry(std).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }   

            return Ok(std);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> deleteStudent(int id)
        {
            try
            {
                var std = await context.Students.FindAsync(id);
                if (std == null) return NotFound();

                context.Students.Remove(std);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

        }
    }
}
