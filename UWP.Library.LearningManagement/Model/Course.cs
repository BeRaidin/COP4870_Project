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

        public void GetMaxGrade()
        {
            MaxGrade = 0;
            foreach(var assignment in Assignments)
            {
                MaxGrade += ((double)assignment.TotalAvailablePoints * (assignment.AssignmentGroup.Weight / (double)100));
            }
        }
    }
}
