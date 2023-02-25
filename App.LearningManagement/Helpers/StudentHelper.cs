using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using static Library.LearningManagement.Models.Student;

namespace App.LearningManagement.Helpers
{
    internal class StudentHelper
    {
        private StudentService studentService;
        private CourseService courseService;

        public StudentHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }

        public void AddOrUpdateStudent(Student? selectedStudent = null)
        {
            if (selectedStudent == null)
            {
                selectedStudent = new Student();
                selectedStudent.ChangeId();
                selectedStudent.ChangeName();
                selectedStudent.ChangeClassification();
                studentService.Add(selectedStudent);
            }
            else
            {
                bool cont = true;
                while (cont)
                {
                    Console.WriteLine("Choose an option:");
                    Console.WriteLine("1. Change Id");
                    Console.WriteLine("2. Change Name");
                    Console.WriteLine("3. Change Classification");
                    Console.WriteLine("4. Finish");
                    var input = Console.ReadLine();

                    if (int.TryParse(input, out int result))
                    {
                        switch (result)
                        {
                            case 1:
                                selectedStudent.ChangeId();
                                break;
                            case 2:
                                selectedStudent.ChangeName();
                                break;
                            case 3:
                                selectedStudent.ChangeClassification();
                                break;
                            case 4:
                                cont = false;
                                break;
                        }
                    }
                }
            }
        }

        public void UpdateStudentRecord()
        {
            Console.WriteLine("Choose the id of student to update:");
            SearchOrListStudents();
            var selectionStr = Console.ReadLine();
            if(int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedStu = studentService.Students.FirstOrDefault(s => s.Id == selectionInt);
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
                studentService.Students.ForEach(Console.WriteLine);
            }
            else
            {
                studentService.Search(query).ToList().ForEach(Console.WriteLine);

                Console.WriteLine("Select a student id to see their classes:");
                var selectionStr = Console.ReadLine();
                var selectionInt = int.Parse(selectionStr ?? "0");

                Console.WriteLine("Student Course List");
                courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
            }
        }
    }
}
