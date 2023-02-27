using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Student : Person
    {
        public Dictionary<string, double> Grades { get; set; }
        public Classes Classification { get; set; }
        public enum Classes
        {
            Freshman, Sophmore, Junior, Senior
        }

        public Student()
        {
            Grades = new Dictionary<string, double>();
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Classification}";
        }


        public void ChangeClassification()
        {
            Console.WriteLine("What is the classification of the student? (F)reshman s(O)phmore (J)unior (S)enior");
            var classification = Console.ReadLine() ?? string.Empty;
            Classes classEnum = Classes.Freshman;
            if (classification.Equals("O", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = Classes.Sophmore;
            }
            else if (classification.Equals("J", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = Classes.Junior;
            }
            else if (classification.Equals("S", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = Classes.Senior;
            }

            Classification = classEnum;
        }

        public void DeleteGrades()
        {
            Grades.Clear();
            Grades = new Dictionary<string, double>();
        }
    }
}
