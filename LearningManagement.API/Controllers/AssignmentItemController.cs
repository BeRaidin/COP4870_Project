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
        public List<AssignmentItem> Get()
        {
            return new List<AssignmentItem>();
        }

        [HttpPost("AddOrUpdate")]
        public AssignmentItem AddorUpdate([FromBody] AssignmentItem assignmentItem)
        {
            return assignmentItem;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] AssignmentItem assignmentItem)
        {
        }
    }
}
