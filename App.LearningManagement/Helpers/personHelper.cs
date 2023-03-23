﻿using Library.LearningManagement.Models;
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

        public Person GetPerson()
        {
            Console.WriteLine("Enter the id of a person:");
            ListPeople();
            var selection = Console.ReadLine();
            while (!int.TryParse(selection, out int selectionInt) || !personService.People.Any(i => i.Id == int.Parse(selection)))
            {
                Console.WriteLine("Please enter a vaild id:");
                selection = Console.ReadLine();
            }
            var selectedPerson = personService.People.First(s => s.Id == int.Parse(selection));
            return selectedPerson;
        }

        public void ListPeople() 
        {
            if (personService.People.Count > 5)
            {
                var navigator = new ListNavigator<Person>(personService.People);
                navigator.ChangePage();
            }
            else
            {
                personService.People.ForEach(Console.WriteLine);
            }
        }

        public void AddPerson()
        {
            Person person;
            Console.WriteLine("What type of person do you want to add?");
            Console.WriteLine("\t(S)tudent");
            Console.WriteLine("\t(T)eaching Assistant");
            Console.WriteLine("\t(I)nstructor");
            var choice = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrEmpty(choice))
            {
                return;
            }
            if (choice.Equals("S", StringComparison.InvariantCultureIgnoreCase))
            {
                person = new Student();
            }
            else if (choice.Equals("T", StringComparison.InvariantCultureIgnoreCase))
            {
                person = new TeachingAssistant();
            }
            else if (choice.Equals("I", StringComparison.InvariantCultureIgnoreCase))
            {
                person = new Instructor();
            }
            else return;
            
            person.UpdateId(personService.Size() + 1, personService.People);
            personService.Add(person);
        }

        public void UpdatePerson(Person person)
        {
            Console.WriteLine("Would you like to change the name? (Y/N)");
            var choice = Console.ReadLine() ?? string.Empty;
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                person.UpdateName();
            }

            var student = person as Student;
            if (student != null)
            {
                Console.WriteLine("Would you like to change the classification? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                        student.UpdateClassification();
                }
            }
        }

        public void SearchPerson(string? query = null)
        {
            var personList = new List<Person>();
            if (query == null)
            {
                personList = personService.People;
            }
            else
            {
                personList = personService.Search(query).ToList();
            }
            personList.ForEach(Console.WriteLine);
            Console.WriteLine("Select an id to see their classes:");
            var selectionStr = Console.ReadLine();
            var selectionInt = int.Parse(selectionStr ?? "0");
            if(personList.Any(i => i.Id == selectionInt))
            {
                var personCourses = courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList();
                if (personCourses.Count > 0)
                {
                    Console.WriteLine("Course List");
                    personCourses.ForEach(Console.WriteLine);
                }
                else
                {
                    Console.WriteLine("This person is not currently enrolled in any courses.");
                }
            }

        }

        public void AddGrades()
        {
            var selectedPerson = GetPerson();
            var selectedStudent = selectedPerson as Student;
            if (selectedStudent != null)
            {
                var studentCourses = courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectedPerson.Id)).ToList();
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

                        selectedStudent.Grades.Add(assingment, scoredGrade);
                    }
                }
            }
            
        }

        public void DisplayGrades()
        {
            var selectedPerson = GetPerson();
            var selectedStudent = selectedPerson as Student;
            if (selectedStudent != null)
            {
                foreach (KeyValuePair<Assignment, double> grade in selectedStudent.Grades)
                {
                    Console.WriteLine("{0} - {1}%", grade.Key.Name, grade.Value);
                }
            }
        }

        public void DeleteSubmisisons()
        {
            var selectedPerson = GetPerson();
            var selectedStudent = selectedPerson as Student;
            if (selectedStudent != null)
            {
                selectedStudent.DeleteGrades();
            }            
        }

        public void GetFinalGrade()
        {
            var selectedPerson = GetPerson();
            var selectedStudent = selectedPerson as Student;
            if (selectedStudent != null)
            {
                Console.WriteLine("What course do you want to get the grade of?");
                var studentCourses = courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectedPerson.Id)).ToList();
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
                    if (selectedStudent.Grades.Any(n => n.Key.Name.Equals(assignment.Name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        double rawScore = (double)selectedStudent.Grades
                            .First(n => n.Key.Name.Equals(assignment.Name, StringComparison.InvariantCultureIgnoreCase)).Value;
                        points += (rawScore* ((double)assignment.TotalAvailablePoints)* assignment.AssignmentGroup.Weight/100);
                    }
                }
                
                var finalGrade = ((double)points / course.MaxGrade);
                selectedStudent.FinalGrades.Add(course, finalGrade);
                Console.WriteLine($"Final grade is {finalGrade}");
            }
        }

        public void GetGradePoint()
        {
            var selectedPerson = GetPerson();
            var selectedStudent = selectedPerson as Student;
            if (selectedStudent != null)
            {
                double GPA = 0;
                var totalHours = 0;
                double totalHonorPoints = 0;
                foreach(KeyValuePair<Course, double> grade in selectedStudent.FinalGrades) 
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
                selectedStudent.GradePointAverage = GPA;
            }            
        }








    }
}
