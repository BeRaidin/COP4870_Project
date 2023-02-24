using Library.LearningManagement.Models;
using Library.LearningManagement.Services;

namespace App.LearningManagement.Helpers
{
    internal class CourseHelper
    {
        private CourseService courseService;
        private StudentService studentService;

        public CourseHelper()
        {
            studentService = StudentService.Current;
            courseService= CourseService.Current;
        }

        public void AddOrUpdateCourse(Course? selectedCourse = null)
        {
            Console.WriteLine("What is the code of the course?");
            var code = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the name of the course?");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the description of the course?");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Which students should be enrolled in the course? ('Q' to quit)");
            var roster = new List<Person>();
            bool contAdding = true;
            while(contAdding)
            {
                studentService.Students.Where(s => !roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                var selection = "Q"; 
                if (studentService.Students.Any(s => !roster.Any(s2 => s2.Id == s.Id)))
                    selection = Console.ReadLine() ?? string.Empty;

                if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                    contAdding = false;
                else
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);

                    if (selectedStudent != null)
                    {
                        roster.Add(selectedStudent);
                    }
                }
            }

            bool isCreate = false;
            if (selectedCourse == null) 
            {
                isCreate = true;
                selectedCourse = new Course();
            }
            
            selectedCourse.Code = code;
            selectedCourse.Name = name;
            selectedCourse.Description = description;
            selectedCourse.Roster = new List<Person>();
            selectedCourse.Roster = roster;

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

        public void SearchCourses()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            courseService.Search(query).ToList().ForEach(Console.WriteLine);
        }

    }
}
