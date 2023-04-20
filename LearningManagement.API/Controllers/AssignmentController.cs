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
            return new List<Assignment>();
        }

        [HttpPost("AddOrUpdate")]
        public Assignment AddorUpdate([FromBody] Assignment assignment)
        {   
            return assignment;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] Assignment assignment)
        {
        }
    }
}
