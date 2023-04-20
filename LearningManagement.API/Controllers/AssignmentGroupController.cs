using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentGroupController
    {
        private readonly ILogger<AssignmentGroupController> _logger;

        public AssignmentGroupController(ILogger<AssignmentGroupController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<AssignmentGroup> Get()
        {
            return new List<AssignmentGroup>();
        }

        [HttpPost("AddOrUpdate")]
        public AssignmentGroup AddorUpdate([FromBody] AssignmentGroup assignmentGroup)
        {
            return assignmentGroup;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] AssignmentGroup assignmentGroup)
        {
        }
    }
}
