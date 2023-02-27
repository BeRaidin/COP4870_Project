using Library.LearningManagement.Models;
using Library.LearningManagement.Services;

namespace App.LearningManagement.Helpers
{
    internal class PersonHelper
    {
        private PersonService personService;
        private CourseService courseService;

        public PersonHelper()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
        }

        public void AddOrUpdateStudent(Person? selectedPerson = null)
        {
            var isNew = false;
            var choice = string.Empty;
            if (selectedPerson == null)
            {
                isNew = true;
                Console.WriteLine("What type of person do you want to add?");
                Console.WriteLine("\t(S)tudent");
                Console.WriteLine("\t(T)eaching Assistant");
                Console.WriteLine("\t(I)nstructor");
                choice = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrEmpty(choice))
                    return;
                if (choice.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                    selectedPerson = new Student();
                else if (choice.Equals("T", StringComparison.InvariantCultureIgnoreCase))
                    selectedPerson = new TeachingAssistant();
                else if (choice.Equals("I", StringComparison.InvariantCultureIgnoreCase))
                    selectedPerson = new Instructor();
                else
                    return;
            }

            if (isNew)
            {
                selectedPerson.ChangeId(personService.Size()+1, personService.People);        
            }

            choice = "Y";
            if (!isNew)
            {
                Console.WriteLine("Would you like to change the name? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
            }
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                selectedPerson.ChangeName();
            }

            if (!isNew && selectedPerson is Student)
            {
                Console.WriteLine("Would you like to change the classification? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
            }
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase) && selectedPerson is Student)
            {
                var student = selectedPerson as Student;
                if (student != null)
                {
                    student.ChangeClassification();
                }
            }

            if (isNew)
                personService.Add(selectedPerson);
        }

        public void UpdateStudentRecord()
        {
            Console.WriteLine("Choose the id of student to update:");
            SearchOrListStudents();
            var selectionStr = Console.ReadLine();
            if(int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedStu = personService.People.FirstOrDefault(s => s.Id == selectionInt);
                if (selectedStu != null)
                {
                    AddOrUpdateStudent(selectedStu);
                }
            }
        }

        public void SearchOrListStudents(string? query = null)
        {
            if (query == null)
            {
                personService.People.ForEach(Console.WriteLine);
            }
            else
            {
                personService.Search(query).ToList().ForEach(Console.WriteLine);

                Console.WriteLine("Select a student id to see their classes:");
                var selectionStr = Console.ReadLine();
                var selectionInt = int.Parse(selectionStr ?? "0");

                Console.WriteLine("Student Course List");
                courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
            }
        }

        public void AddGrades()
        {
            Console.WriteLine("Choose the id of student to update:");
            SearchOrListStudents();
            var selectionStr = Console.ReadLine();
            if (int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedPerson = personService.People.FirstOrDefault(s => s.Id == selectionInt);
                var selectedStu = selectedPerson as Student;
                if (selectedStu != null)
                {
                    var studentCourses = courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList();
                    foreach(var course in studentCourses)
                    {
                        Console.WriteLine(course);
                        foreach(var assingment in course.Assignments)
                        {
                            Console.WriteLine("\t" + assingment.Name);
                            Console.WriteLine($"Please enter a grade for the assignment out of {assingment.TotalAvailablePoints}:");
                            var grade = Console.ReadLine();
                            while(!int.TryParse(grade, out int result) || int.Parse(grade) > assingment.TotalAvailablePoints || int.Parse(grade) < 0)
                            {
                                Console.WriteLine($"Please enter a valid grade out of {assingment.TotalAvailablePoints}:");
                                grade = Console.ReadLine();
                            }
                            selectedStu.Grades.Add(assingment.Name, Double.Parse(grade));
                        }
                    }   
                }
            }
        }

        public void DisplayGrades()
        {
            Console.WriteLine("Choose the id of student to update:");
            SearchOrListStudents();
            var selectionStr = Console.ReadLine();
            if (int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedPerson = personService.People.FirstOrDefault(s => s.Id == selectionInt);
                var selectedStu = selectedPerson as Student;
                if (selectedStu != null)
                {
                    foreach (KeyValuePair<string, double> grade in selectedStu.Grades)
                    {
                        Console.WriteLine("{0}, Vaue: {1}",
                            grade.Key, grade.Value);
                    }
                }
            }
        }

        public void DeleteSubmisisons()
        {
            Console.WriteLine("Choose the id of student to update:");
            SearchOrListStudents();
            var selectionStr = Console.ReadLine();
            if (int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedPerson = personService.People.FirstOrDefault(s => s.Id == selectionInt);
                var selectedStu = selectedPerson as Student;
                if (selectedStu != null)
                {
                    selectedStu.DeleteGrades();
                }
            }
        }
    }
}
