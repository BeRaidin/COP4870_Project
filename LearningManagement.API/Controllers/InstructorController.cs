using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstructorController
    {
        private readonly ILogger<InstructorController> _logger;

        public InstructorController(ILogger<InstructorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Instructor> Get()
        {
            return new InstructorEC().GetInstructors();
        }

        [HttpPost("AddOrUpdate")]
        public Person AddorUpdate([FromBody] Instructor instructor)
        {
            return new InstructorEC().AddorUpdateAdmin(instructor);
        }

        [HttpPost("Delete")]
        public void DeleteInstructor([FromBody] Instructor instructor)
        {
            new InstructorEC().Delete(instructor);
        }
    }
}
