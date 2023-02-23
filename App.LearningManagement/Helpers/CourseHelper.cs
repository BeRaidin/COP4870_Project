using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using static Library.LearningManagement.Models.Person;

namespace App.LearningManagement.Helpers
{
    internal class CourseHelper
    {
        private CourseService courseService = new CourseService();

        public void CreateCourseRecord()
        {
            Console.WriteLine("What is the code of the course?");
            var code = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the name of the course?");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the description of the course?");
            var description = Console.ReadLine() ?? string.Empty;

            var course = new Course
            {
                Code = code,
                Name = name,
                Description = description
            };

            courseService.Add(course);
        }

        public void ListCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }

    }
}
