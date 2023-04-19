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

        [HttpPost]
        public Person AddorUpdate([FromBody] Instructor dto)
        {
            return new InstructorEC().AddorUpdateAdmin(dto);
        }
    }
}
