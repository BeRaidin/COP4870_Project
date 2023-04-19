using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.DTO;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachingAssistantController
    {
        private readonly ILogger<InstructorController> _logger;

        public TeachingAssistantController(ILogger<InstructorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<TeachingAssistantDTO> Get()
        {
            return new InstructorEC().GetAssistants();
        }

        [HttpPost]
        public PersonDTO AddorUpdate([FromBody] TeachingAssistantDTO dto)
        {
            return new InstructorEC().AddorUpdateAdmin(dto);
        }
    }
}
