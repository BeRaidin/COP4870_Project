using App.LearningManagement.Helpers;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var studentHelper = new StudentHelper();
            var courseHelper = new CourseHelper();

            bool cont = true;
            while (cont)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a new course");
                Console.WriteLine("2. Add a student enrollment");
                Console.WriteLine("3. List all courses");
                Console.WriteLine("4. List all enrolled students");
                Console.WriteLine("5. Search for courses");
                Console.WriteLine("6. Search for student");
                Console.WriteLine("7. Update a course");
                Console.WriteLine("8. Update a student enrollment");
                Console.WriteLine("9. Exit");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    switch (result)
                    {
                        case 1:
                            courseHelper.AddOrUpdateCourse();
                            break;
                        case 2:
                            studentHelper.AddOrUpdateStudent();
                            break;
                        case 3:
                            courseHelper.SearchOrListCourses();
                            break;
                        case 4:
                            studentHelper.ListStudents();
                            break;
                        case 5:
                            Console.WriteLine("Enter a query:");
                            var query = Console.ReadLine() ?? string.Empty;
                            courseHelper.SearchOrListCourses(query);
                            break;
                        case 6:
                            studentHelper.SearchStudents();
                            break;
                        case 7:
                            courseHelper.UpdateCourseRecord();
                            break;
                        case 8:
                            studentHelper.UpdateStudentRecord();
                            break;
                        case 9:
                            cont = false;
                            break;
                    }
                }
            }
        }
    }
}