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
            Console.WriteLine("7. Delete student submissions");
            Console.WriteLine("8. Get final grade for a student in a course");
            Console.WriteLine("9. Get Grade Point Average");

            var input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                switch (result)
                {
                    case 1:
                        personHelper.AddPerson();
                        break;
                    case 2:
                        personHelper.ListPeople();
                        break;
                    case 3:
                        Console.WriteLine("Enter a query:");
                        var query = Console.ReadLine() ?? string.Empty;
                        personHelper.SearchPerson(query);
                        break;
                    case 4:
                        personHelper.UpdatePerson(personHelper.GetPerson());
                        break;
                    case 5:
                        personHelper.AddGrades();
                        break;
                    case 6:
                        personHelper.DisplayGrades();
                        break;
                    case 7:
                        personHelper.DeleteSubmisisons();
                        break;
                    case 8:
                        personHelper.GetFinalGrade();
                        break;
                    case 9:
                        personHelper.GetGradePoint();
                        break;
                }
            }
        }

        static void RunCourses(CourseHelper courseHelper)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1.  Add a new course");
            Console.WriteLine("2.  List all courses");
            Console.WriteLine("3.  Search for courses");
            Console.WriteLine("4.  Update a course");
            Console.WriteLine("5.  Add students to a course");
            Console.WriteLine("6.  Add an assignment");
            Console.WriteLine("7.  Remove a student from a course");
            Console.WriteLine("8.  Add a module to a course");
            Console.WriteLine("9.  Edit a module");
            Console.WriteLine("10. Add an announcement to a course");
            Console.WriteLine("11. Edit an announcement");
            Console.WriteLine("12. Delete a module");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                switch (result)
                {
                    case 1:
                        courseHelper.AddCourse();
                        break;
                    case 2:
                        courseHelper.ListCourses();
                        break;
                    case 3:
                        
                        courseHelper.SearchCourses();
                        break;
                    case 4:
                        courseHelper.UpdateCourse(courseHelper.GetCourse());
                        break;
                    case 5:
                        courseHelper.AddStudents(courseHelper.GetCourse());
                        break;
                    case 6:
                        courseHelper.AddAssignments(courseHelper.GetCourse());
                        break;
                    case 7:
                        courseHelper.RemoveStudent(courseHelper.GetCourse());
                        break;
                    case 8:
                        courseHelper.AddOrUpdateModule();
                        break;
                    case 9:
                        courseHelper.UpdateCourseModule();
                        break;
                    case 10:
                        courseHelper.AddOrUpdateAnnouncement();
                        break;
                    case 11:
                        bool update = true;
                        courseHelper.AddOrUpdateAnnouncement(update);
                        break;
                    case 12:
                        courseHelper.DeleteModule();
                        break;
                }
            }
        }
    }
}