using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.DTO;
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
        public List<InstructorDTO> Get()
        {
            return new InstructorEC().GetInstructors();
        }

        [HttpPost]
        public InstructorDTO AddorUpdate([FromBody] InstructorDTO dto)
        {
            return new InstructorEC().AddorUpdateInstructor(dto);
        }
    }
}
