using System.Collections.Generic;
using System.Linq;

namespace UWP.Library.LearningManagement.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public int CreditHours { get; set; }
        public string Description { get; set; }
        public List<Person> Roster { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Module> Modules { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; }
        public List<Announcement> Announcements { get; set; }
        public double MaxGrade { get; set; }
        public bool IsSelected { get; set; }

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
            IsSelected = false;
        }

        public virtual string Display => $"[{Code}] - {Name}";

        public void Add(Announcement announcement)
        {
            var previousAnnouncements = Announcements.FirstOrDefault(x => x.Id == announcement.Id);
            if (previousAnnouncements != null)
            {
                Announcements.Remove(previousAnnouncements);
            }
            Announcements.Add(announcement);
        }

        public void Add(Person person)
        {
            Roster.Add(person);
        }

        public void Add(Assignment assignment)
        {
            var previousAssignment = Assignments.FirstOrDefault(x => x.Id == assignment.Id);
            if (previousAssignment != null)
            {
                Assignments.Remove(previousAssignment);
            }
            Assignments.Add(assignment);
        }

        public void Add(Module module)
        {
            var previousModule = Modules.FirstOrDefault(x => x.Id == module.Id);
            if (previousModule != null)
            {
                Modules.Remove(previousModule);
            }
            Modules.Add(module);
        }

        public void Remove(Person person)
        {
            Person deletedPerson = Roster.FirstOrDefault(x => x.Id == person.Id);
            Roster.Remove(deletedPerson);
        }

        public void Remove(Assignment assignment)
        {
            var removedAssignment = Assignments.FirstOrDefault(x => x.Id == assignment.Id);
            if (removedAssignment != null)
            {
                Assignments.Remove(removedAssignment);
            }

            //foreach (var person in Roster)
            //{
            //    if (person is Student student)
            //    {
            //        student.Remove(assignment);
            //    }
            //}
        }

        public void Remove(Announcement announcement)
        {
            var removedAnnouncement = Announcements.FirstOrDefault(x => x.Id == announcement.Id);
            if (removedAnnouncement != null)
            {
                Announcements.Remove(removedAnnouncement);
            }
        }

        public void Remove(Module module)
        {
            var removedModule = Modules.FirstOrDefault(x => x.Id == module.Id);
            if (removedModule != null)
            {
                Modules.Remove(removedModule);
            }
        }

        public void GetMaxGrade()
        {
            MaxGrade = 0;
            foreach (var assignment in Assignments)
            {
                MaxGrade += assignment.TotalAvailablePoints * (assignment.AssignmentGroup.Weight / (double)100);
            }
        }
    }
}
