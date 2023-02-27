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
                selectedPerson.ChangeId(personService.Size() + 1, personService.People);
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
            if (int.TryParse(selectionStr, out int selectionInt))
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
                    foreach (var course in studentCourses)
                    {
                        Console.WriteLine(course);
                        foreach (var assingment in course.Assignments)
                        {
                            Console.WriteLine("\t" + assingment.Name);
                            Console.WriteLine($"Please enter a grade for the assignment out of {assingment.TotalAvailablePoints}:");
                            var grade = Console.ReadLine();
                            while (!int.TryParse(grade, out int result) || int.Parse(grade) > assingment.TotalAvailablePoints || int.Parse(grade) < 0)
                            {
                                Console.WriteLine($"Please enter a valid grade out of {assingment.TotalAvailablePoints}:");
                                grade = Console.ReadLine();
                            }
                            double scoredGrade = ((double)int.Parse(grade) / assingment.TotalAvailablePoints) * 100;

                            selectedStu.Grades.Add(assingment, scoredGrade);
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
                    foreach (KeyValuePair<Assignment, double> grade in selectedStu.Grades)
                    {
                        Console.WriteLine("{0} - {1}%",
                            grade.Key.Name, grade.Value, grade.Key.TotalAvailablePoints);
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

        public void GetFinalGrade()
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
                    Console.WriteLine("What course do you want to get the grade of?");
                    var studentCourses = courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList();
                    studentCourses.ForEach(Console.WriteLine);

                    var selectedCourse = Console.ReadLine() ?? string.Empty;
                    while (!studentCourses.Any(c => c.Code.Equals(selectedCourse, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        Console.WriteLine("Please choose a valid code:");
                        selectedCourse = Console.ReadLine() ?? string.Empty;
                    }
                    var course = studentCourses.First(c => c.Code.Equals(selectedCourse, StringComparison.InvariantCultureIgnoreCase));
                    double points = 0;
                    foreach(var assignment in course.Assignments)
                    {
                        if (selectedStu.Grades.Any(n => n.Key.Name.Equals(assignment.Name, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            double rawPoints = (double)selectedStu.Grades
                                .First(n => n.Key.Name.Equals(assignment.Name, StringComparison.InvariantCultureIgnoreCase)).Value;
                            points += rawPoints* ((double)assignment.AssignmentGroup.Weight);
                        }
                    }

                    var finalGrade = (double)points / course.MaxGrade;
                    selectedStu.FinalGrades.Add(course, finalGrade);
                    Console.WriteLine($"Final grade is {finalGrade}");
                }
            }
        }

        public void GetGradePoint()
        {
            var letterValues = new Dictionary<string, double>(){{"A", 4.0},{"B", 3.0},{"C", 2.0},{"D", 1.0},{"F", 0.0}};
            Console.WriteLine("Choose the id of student to update:");
            SearchOrListStudents();
            var selectionStr = Console.ReadLine();
            if (int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedPerson = personService.People.FirstOrDefault(s => s.Id == selectionInt);
                var selectedStu = selectedPerson as Student;
                if (selectedStu != null)
                {
                    double GPA = 0;
                    var totalHours = 0;
                    double totalHonorPoints = 0;
                    foreach(KeyValuePair<Course, double> grade in selectedStu.FinalGrades) 
                    {
                        var courseHonorPoints = (double)0.0;
                        totalHours += grade.Key.CreditHours;
                        if(grade.Value < 70)
                        {
                            courseHonorPoints = 0.0;
                        } 
                        else if(grade.Value < 80)
                        {
                            courseHonorPoints = 2.0;
                        }
                        else if (grade.Value < 90)
                        {
                            courseHonorPoints = 3.0;
                        }
                        else
                        {
                            courseHonorPoints = 4.0;
                        }
                        totalHonorPoints += (double)courseHonorPoints * grade.Key.CreditHours;
                    }
                    GPA = (double)totalHonorPoints / totalHours;
                    Console.WriteLine("The students GPA is " + GPA);
                    selectedStu.GradePointAverage = GPA;
                }
            }
        }
    }
}
