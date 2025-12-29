using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students_CRUD_WebAPI.Data;
using Students_CRUD_WebAPI.Models;

namespace Students_CRUD_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentModelController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public StudentModelController(ApiDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL STUDENTS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentModel>>> GetStudents()
        {
            return await _context.StudentModel.ToListAsync();
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentModel>> GetStudent(int id)
        {
            var student = await _context.StudentModel.FindAsync(id);
            if (student == null)
                return NotFound();

            return student;
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<StudentModel>> Create(StudentModel student)
        {
            _context.StudentModel.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.ID }, student);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StudentModel student)
        {
            if (id != student.ID)
                return BadRequest();

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.StudentModel.FindAsync(id);
            if (student == null)
                return NotFound();

            _context.StudentModel.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
