using Microsoft.AspNetCore.Mvc;
using W3.model;

namespace W3.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class StudentController : Controller
    {
        public static List<Student> students = new List<Student>();
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(students);
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            var s = new Student()
            {
                Id = Guid.NewGuid(),
                Name = student.Name
            };
            students.Add(s);
            return Ok(new
            {
                Success = true, Data = s
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var student = students.SingleOrDefault(s => s.Id == Guid.Parse(id));
                if (student == null)
                {
                    return NotFound();
                }
                return Ok(student);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id,Student studentEdit)
        {
            try
            {
                var student = students.SingleOrDefault(s => s.Id == Guid.Parse(id));
                if (student == null)
                {
                    return NotFound();
                }

                if(student.Id != Guid.Parse(id))
                {
                    return BadRequest();
                }
                student.Name = studentEdit.Name;

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var student = students.SingleOrDefault(s => s.Id == Guid.Parse(id));
                if (student == null)
                {
                    return NotFound();
                }
                students.Remove(student);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
