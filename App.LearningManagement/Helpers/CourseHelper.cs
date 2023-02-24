using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using static Library.LearningManagement.Models.Person;

namespace App.LearningManagement.Helpers
{
    internal class CourseHelper
    {
        private CourseService courseService = new CourseService();

        public void AddOrUpdateCourse(Course? selectedCourse = null)
        {
            Console.WriteLine("What is the code of the course?");
            var code = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the name of the course?");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the description of the course?");
            var description = Console.ReadLine() ?? string.Empty;

            bool isCreate = false;
            if (selectedCourse == null) 
            {
                isCreate = true;
                selectedCourse = new Course();
            }
            
            selectedCourse.Code = code;
            selectedCourse.Name = name;
            selectedCourse.Description = description;

            if (isCreate)
                courseService.Add(selectedCourse);
        }

        public void UpdateCourseRecord()
        {
            Console.WriteLine("Choose the code of course to update:");
            ListCourses();
            var selection = Console.ReadLine();
            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));

                if (selectedCourse != null)
                {
                    AddOrUpdateCourse(selectedCourse);
                }
        }

        public void ListCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }

    }
}
