using LearningManagement.API.EC;
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
        [HttpGet("GetList")]
        public List<Semester> GetList()
        {
            return new SemesterEC().GetSemesters();
        }
        [HttpGet("GetCurrentSemester")]
        public List<Semester> GetCurrentSemester()
        {
            return new SemesterEC().GetCurrentSemester();
        }

        [HttpPost("Add")]
        public Semester Add([FromBody] Semester semester)
        {
            return new SemesterEC().AddSemester(semester);
        }
        [HttpPost("SetCurrent")]
        public Semester SetCurrent([FromBody] Semester semester)
        {
            return new SemesterEC().SetCurrent(semester);
        }
    }
}
