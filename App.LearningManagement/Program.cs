using App.LearningManagement.Helpers;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var personHelper = new PersonHelper();
            var courseHelper = new CourseHelper();

            bool cont = true;
            while (cont)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. People");
                Console.WriteLine("2. Courses");
                Console.WriteLine("3. Finish");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        RunPeople(personHelper);
                    }
                    else if (result == 2)
                    {
                        RunCourses(courseHelper);
                    }
                    else if (result == 3)
                    {
                        cont = false;
                    }
                }
            }
        }

        static void RunPeople(PersonHelper personHelper)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add a new person");
            Console.WriteLine("2. List all people");
            Console.WriteLine("3. Search for people");
            Console.WriteLine("4. Update a person");
            Console.WriteLine("5. Assign grades to a student");
            Console.WriteLine("6. List grades for a student");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                switch (result)
                {
                    case 1:
                        personHelper.AddOrUpdateStudent();
                        break;
                    case 2:
                        personHelper.SearchOrListStudents();
                        break;
                    case 3:
                        Console.WriteLine("Enter a query:");
                        var query = Console.ReadLine() ?? string.Empty;
                        personHelper.SearchOrListStudents(query);
                        break;
                    case 4:
                        personHelper.UpdateStudentRecord();
                        break;
                    case 5:
                        personHelper.AddGrades();
                        break;
                    case 6:
                        personHelper.DisplayGrades();
                        break;
                }
            }
        }

        static void RunCourses(CourseHelper courseHelper)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add a new course");
            Console.WriteLine("2. List all courses");
            Console.WriteLine("3. Search for courses");
            Console.WriteLine("4. Update a course");
            Console.WriteLine("5. Add a module to a course");
            Console.WriteLine("6. Edit a module");
            Console.WriteLine("7. Add an announcement to a course");
            Console.WriteLine("8. Edit an announcement");


            var input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                switch (result)
                {
                    case 1:
                        courseHelper.AddOrUpdateCourse();
                        break;
                    case 2:
                        courseHelper.SearchOrListCourses();
                        break;
                    case 3:
                        Console.WriteLine("Enter a query:");
                        var query = Console.ReadLine() ?? string.Empty;
                        courseHelper.SearchOrListCourses(query);
                        break;
                    case 4:
                        courseHelper.AddOrUpdateCourse(courseHelper.GetCourse());
                        break;
                    case 5:
                        courseHelper.AddOrUpdateModule();
                        break;
                    case 6:
                        courseHelper.UpdateCourseModule();
                        break;
                    case 7:
                        courseHelper.AddOrUpdateAnnouncement();
                        break;
                    case 8:
                        bool update = true;
                        courseHelper.AddOrUpdateAnnouncement(update);
                        break;
                }
            }
        }
    }
}