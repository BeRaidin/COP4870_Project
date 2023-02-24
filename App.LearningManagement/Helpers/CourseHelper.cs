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
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }

                if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    contAdding = false;
                }
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

            Console.WriteLine("Would you like to add assignments? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            var assignments = new List<Assignment>();
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                contAdding = true;
                while(contAdding)
                {
                    Console.WriteLine("Name:");
                    var assignmentName = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Description:");
                    var assignmentDescription = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Total Points:");
                    var totalPoints = decimal.Parse(Console.ReadLine() ?? "100");
                    Console.WriteLine("Due Date:");
                    var dueDate = DateTime.Parse(Console.ReadLine() ?? "01/01/1990");

                    assignments.Add(new Assignment
                    {
                        Name = assignmentName,
                        Description = assignmentDescription,
                        TotalAvailablePoints = totalPoints,
                        DueDate = dueDate
                    });

                    Console.WriteLine("Add more assignments? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        contAdding = false;
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
            selectedCourse.Assignments = new List<Assignment>();
            selectedCourse.Assignments.AddRange(assignments);

            if (isCreate)
            {
                courseService.Add(selectedCourse);
            }
        }

        public void UpdateCourseRecord()
        {
            Console.WriteLine("Choose the code of course to update:");
            SearchOrListCourses();
            var selection = Console.ReadLine();
            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
                if (selectedCourse != null)
                {
                    AddOrUpdateCourse(selectedCourse);
                }
        }

        public void SearchOrListCourses(string? query = null)
        {
            if (query == null)
            {
                courseService.Courses.ForEach(Console.WriteLine);
            }
            else
            {
                courseService.Search(query).ToList().ForEach(Console.WriteLine);
            }

            Console.WriteLine("Select a course:");
            var code = Console.ReadLine() ?? string.Empty;
            var selectedCourse = courseService.Courses
                .FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine(selectedCourse.DetailDisplay);
            }
        }

    }
}
