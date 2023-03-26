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

        public void AddMaxGrade(double val)
        {
            MaxGrade += val;
        }
    }
}
