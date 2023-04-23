using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            return new CourseEC().GetCourses();
        }

        [HttpPost("AddOrUpdate")]
        public Course AddorUpdate([FromBody] Course course)
        {
            return new CourseEC().AddOrUpdateCourse(course);
        }

        [HttpPost("Delete")]
        public void DeleteInstructor([FromBody] Course course)
        {
        }
    }
}
