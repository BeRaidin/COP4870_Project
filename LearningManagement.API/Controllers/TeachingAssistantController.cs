using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

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
        public List<TeachingAssistant> Get()
        {
            return new EC.PersonEC().GetAssistants();
        }

        [HttpPost("AddOrUpdate")]
        public Person AddorUpdate([FromBody] TeachingAssistant assistant)
        {
            return new PersonEC().AddorUpdateAdmin(assistant);
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] TeachingAssistant assistant)
        {
            new PersonEC().Delete(assistant);
        }


    }
}
