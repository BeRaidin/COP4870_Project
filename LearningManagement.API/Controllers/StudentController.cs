using Microsoft.AspNetCore.Mvc;
using LearningManagement.API.EC;
using UWP.Library.LearningManagement.DTO;

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
        public List<StudentDTO> Get()
        {
            return new StudentEC().GetStudents();
        }

        [HttpPost]
        public StudentDTO AddorUpdate([FromBody] StudentDTO dto) 
        { 
            return new StudentEC().AddorUpdateStudent(dto);
        }
    }
}
