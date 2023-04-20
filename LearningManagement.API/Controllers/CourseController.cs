using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController
    {
        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Course> Get()
        {
            return new List<Course>();
        }

        [HttpPost("AddOrUpdate")]
        public Course AddorUpdate([FromBody] Course course)
        {
            return course;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] Course course)
        {
        }
    }
}
