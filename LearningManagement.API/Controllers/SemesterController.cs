using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SemesterController
    {
        private readonly ILogger<SemesterController> _logger;

        public SemesterController(ILogger<SemesterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Semester> Get()
        {
            return new List<Semester>();
        }

        [HttpPost("AddOrUpdate")]
        public Semester AddorUpdate([FromBody] Semester semester)
        {
            return semester;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] Semester semester)
        {
        }
    }
}
