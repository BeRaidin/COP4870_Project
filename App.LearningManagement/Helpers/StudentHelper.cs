using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.LearningManagement.Models.Person;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;

namespace App.LearningManagement.Helpers
{
    internal class StudentHelper
    {
        private StudentService studentService = new StudentService();
        public void CreateStudentRecord()
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

            var student = new Person
            {
                Id = int.Parse(id ?? "0"),
                Name = name ?? string.Empty,
                Classification = classEnum
            };

            studentService.Add(student);
        }

        public void ListStudents()
        {
            studentService.Students.ForEach(Console.WriteLine);
        }

    }
}
