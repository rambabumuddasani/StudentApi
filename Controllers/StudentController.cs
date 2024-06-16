using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "John Doe", Age = 20, Class = "Math" },
            new Student { Id = 2, Name = "Jane Smith", Age = 22, Class = "Science" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return students;
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student student)
        {
            student.Id = students.Count + 1;
            students.Add(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Student updatedStudent)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Class = updatedStudent.Class;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            students.Remove(student);
            return NoContent();
        }
    }
}
