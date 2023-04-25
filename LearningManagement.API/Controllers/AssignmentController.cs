using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController
    {
        private readonly ILogger<AssignmentController> _logger;

        public AssignmentController(ILogger<AssignmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Assignment> Get()
        {
            return new AssignmentEC().GetAssignments();
        }

        [HttpPost("AddOrUpdate")]
        public Assignment AddorUpdate([FromBody] Assignment assignment)
        {
            return new AssignmentEC().AddOrUpdateAssignment(assignment);
        }

        [HttpPost("Delete")]
        public void DeleteAssignment([FromBody] Assignment assignment)
        {
            new AssignmentEC().Delete(assignment);
        }
    }
}
