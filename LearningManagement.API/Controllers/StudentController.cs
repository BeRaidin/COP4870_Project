using Microsoft.AspNetCore.Mvc;
using LearningManagement.API.EC;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Student> Get()
        {
            return new PersonEC().GetStudents();
        }

        [HttpPost("AddOrUpdate")]
        public Student AddorUpdate([FromBody] Student student) 
        { 
            return new PersonEC().AddorUpdateStudent(student);
        }

        [HttpPost("Delete")]
        public void DeleteInstructor([FromBody] Student student)
        {
            new PersonEC().Delete(student);
        }
    }
}
