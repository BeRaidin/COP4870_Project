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

        [HttpPost("UpdateRoster")]
        public Course UpdateRoster([FromBody] Course course)
        {
            return new CourseEC().UpdateRoster(course);
        }
        [HttpPost("UpdateAnnouncements")]
        public Course UpdateAnnouncements([FromBody] Course course)
        {
            return new CourseEC().UpdateAnnouncements(course);
        }
        [HttpPost("UpdateModules")]
        public Course UpdateModules([FromBody] Course course)
        {
            return new CourseEC().UpdateModules(course);
        }
        [HttpPost("UpdateAssignments")]
        public Course UpdateAssignments([FromBody] Course course)
        {
            return new CourseEC().UpdateAssignments(course);
        }


        [HttpPost("Delete")]
        public void DeleteCourse([FromBody] Course course)
        {
            new CourseEC().Delete(course);
        }
    }
}
