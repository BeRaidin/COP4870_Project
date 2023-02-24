using static Library.LearningManagement.Models.Person;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;

namespace App.LearningManagement.Helpers
{
    internal class StudentHelper
    {
        private StudentService studentService = new StudentService();
        public void AddOrUpdateStudent(Person? selectedStudent = null)
        {
            Console.WriteLine("What is the ID of the student");
            var id = Console.ReadLine();

            Console.WriteLine("What is the name of the student?");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("What is the classification of the student? (F)reshman s(O)phmore (J)unior (S)enior");
            var classification = Console.ReadLine() ?? string.Empty;
            Classes classEnum = Classes.Freshman;
            if (classification.Equals("O", StringComparison.InvariantCultureIgnoreCase))
                classEnum = Classes.Sophmore;
            else if (classification.Equals("J", StringComparison.InvariantCultureIgnoreCase))
                classEnum = Classes.Junior;
            else if (classification.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                classEnum = Classes.Senior;

            bool isCreate = false;
            if(selectedStudent == null)
            {
                isCreate = true;
                selectedStudent = new Person();
            }

            selectedStudent.Id = int.Parse(id ?? "0");
            selectedStudent.Name = name ?? string.Empty;
            selectedStudent.Classification = classEnum;

            if(isCreate)
                studentService.Add(selectedStudent);
        }

        public void UpdateStudentRecord()
        {
            Console.WriteLine("Choose the id of student to update:");
            ListStudents();
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

        public void ListStudents()
        {
            studentService.Students.ForEach(Console.WriteLine);
        }

        public void SearchStudents()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            studentService.search(query).ToList().ForEach(Console.WriteLine);
        }

    }
}
