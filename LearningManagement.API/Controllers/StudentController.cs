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
            return new StudentEC().GetStudents();
        }

        [HttpPost]
        public Student AddorUpdate([FromBody] Student dto) 
        { 
            return new StudentEC().AddorUpdateStudent(dto);
        }
    }
}
