using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public string Description { get; set; }
        public List<Person> Roster { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Module> Modules { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; }
        public List<Announcement> Announcements { get; set; }
        public double MaxGrade { get; set; }

        public Course() 
        { 
            Code = string.Empty;
            Name = string.Empty;
            CreditHours = 3;
            Description = string.Empty;
            Roster = new List<Person>();
            Assignments = new List<Assignment>();
            Modules = new List<Module>();
            AssignmentGroups = new List<AssignmentGroup>();
            Announcements = new List<Announcement>();
            MaxGrade = 0;
        }

        public virtual string Display => $"[{Code}] - {Name}";

        public string DetailDisplay
        {
            get
            {
                var display = $"{ToString()}\n{Description}";
                if(Roster.Count > 0)
                {
                    display += $"\n\nRoster:\n{string.Join("\n", Roster.Select(s => s.ToString()).ToArray())}\n\n";

                }
                if(Assignments.Count > 0)
                {
                    display += $"Assignments:\n{string.Join("\n", Assignments.Select(a => a.ToString()).ToArray())}\n\n";

                }
                if(Modules.Count > 0)
                {
                    display += $"Modules:\n{string.Join("\n", Modules.Select(a => a.DetailDisplay).ToArray())}\n\n";
                }
                if(Announcements.Count > 0)
                {
                    display += $"Announcements:\n{string.Join("\n", Announcements.Select(a => a.ToString()).ToArray())}";
                }
                return display;
            }
        }

        public bool CheckCode(IList<Course> courses)
        {
            if (courses.Any(p => p.Code == Code))
            {
                return false;
            }
            else return true;
        }

        public void ChangeCode(List<Course> course)
        {
            Console.WriteLine("What is the code of the course?");
            var code = Console.ReadLine() ?? string.Empty;

            while (course.Any(p => p.Code == code))
            {
                Console.WriteLine("That is already another course code, please enter another");
                code = Console.ReadLine() ?? string.Empty;
            }

            Code = code;
        }

        public void ChangeName()
        {
            Console.WriteLine("What is the name of the course?");
            Name = Console.ReadLine() ?? string.Empty;
        }

        public void ChangeDescription()
        {
            Console.WriteLine("What is the description of the course?");
            Description = Console.ReadLine() ?? string.Empty;
        }

        public void ChangeHours()
        {
            Console.WriteLine("How many credit hours is the course?");
            var hours = Console.ReadLine() ?? string.Empty;
            while (!int.TryParse(hours, out int result))
            {
                Console.WriteLine("Please enter an integer:");
                hours = Console.ReadLine() ?? string.Empty;
            }
            CreditHours = int.Parse(hours);
        }

        public void AddMaxGrade(double val)
        {
            MaxGrade += val;
        }
    }
}
