using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentItemController
    {
        private readonly ILogger<AssignmentItemController> _logger;

        public AssignmentItemController(ILogger<AssignmentItemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Instructor> Get()
        {
            return new PersonEC().GetInstructors();
        }

        [HttpPost("AddOrUpdate")]
        public Person AddorUpdate([FromBody] Instructor instructor)
        {
            return new PersonEC().AddorUpdateAdmin(instructor);
        }

        [HttpPost("Delete")]
        public void DeleteInstructor([FromBody] Instructor instructor)
        {
            new PersonEC().Delete(instructor);
        }
    }
}
