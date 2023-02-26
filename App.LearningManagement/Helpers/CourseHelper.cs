using Library.LearningManagement.Models;
using Library.LearningManagement.Services;

namespace App.LearningManagement.Helpers
{
    internal class CourseHelper
    {
        private CourseService courseService;
        private PersonService personService;

        public CourseHelper()
        {
            personService = PersonService.Current;
            courseService= CourseService.Current;
        }

        public void AddOrUpdateCourse(Course? selectedCourse = null)
        {
            if (selectedCourse == null)
            {
                selectedCourse = new Course();
                selectedCourse.ChangeCode(courseService.Courses);
                selectedCourse.ChangeName();
                selectedCourse.ChangeDescription();

                Console.WriteLine("Which students should be enrolled in the course? ('Q' to quit)");
                var roster = new List<Person>();
                bool contAdding = true;
                while (contAdding)
                {
                    personService.People.Where(s => !roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                    var selection = "Q";
                    if (personService.People.Any(s => !roster.Any(s2 => s2.Id == s.Id)))
                    {
                        selection = Console.ReadLine() ?? string.Empty;
                        if(selection == string.Empty)
                        {
                            selection = "Q";
                        }
                    }

                    if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                    {
                        contAdding = false;
                    }
                    else
                    {
                        var selectedId = int.Parse(selection);
                        var selectedStudent = personService.People.FirstOrDefault(s => s.Id == selectedId);

                        if (selectedStudent != null)
                        {
                            roster.Add(selectedStudent);
                        }
                    }
                }
                selectedCourse.Roster = roster;

                Console.WriteLine("Would you like to add assignments? (Y/N)");
                var assignResponse = Console.ReadLine() ?? "N";
                var assignments = new List<Assignment>();
                if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    contAdding = true;
                    while (contAdding)
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

                selectedCourse.Assignments.AddRange(assignments);

                courseService.Add(selectedCourse);
            }
            else
            {
                Console.WriteLine("Would you like to change the Code? (Y/N)");
                var choice = Console.ReadLine() ?? string.Empty;
                if(choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedCourse.ChangeCode(courseService.Courses);
                }
                Console.WriteLine("Would you like to change the Name? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedCourse.ChangeName();
                }
                Console.WriteLine("Would you like to change the Description? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedCourse.ChangeDescription();
                }
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

                Console.WriteLine("Select a course to see more information:");
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
}
