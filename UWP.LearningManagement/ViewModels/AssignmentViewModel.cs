using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace UWP.LearningManagement.ViewModels
{
    public class AssignmentViewModel
    {
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;
        private Assignment _assignment;
        public Assignment Assignment
        {
            get { return _assignment; }
            set { _assignment = value; }
        }
        public AssignmentItem Item { get; set; }
        public string Name
        {
            get { return moduleService.CurrentItem.Name; }
            set { moduleService.CurrentItem.Name = value; }
        }
        public string Description
        {
            get { return moduleService.CurrentItem.Description; }
            set { moduleService.CurrentItem.Description = value; }
        }
        public string TotalPoints { get; set; }
        public string Group { get; set; }
        public DateTimeOffset DueDate {
            get { return Assignment.DueDate; }
            set { Assignment.DueDate = value; }
        }

        public AssignmentViewModel()
        {
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            Item = moduleService.CurrentItem as AssignmentItem;
            Assignment = new Assignment();
            DueDate = DateTimeOffset.Now;
        }

        public void Set()
        {
            Assignment.Name = Name;
            Assignment.Description = Description;
            if(int.TryParse(TotalPoints, out var totalPoints))
            {
                Assignment.TotalAvailablePoints = totalPoints;
            }
            else
            {
                Assignment.TotalAvailablePoints = 100;
            }
        }

        public void Add()
        {
            Set();
            if(Item != null)
            {
                Item.Assignment = Assignment;
            }
            foreach (Person person in courseService.CurrentCourse.Roster)
            {
                var student = person as Student;
                if (student != null)
                {
                    student.Grades.Add(Assignment, 0);
                }
            }
            courseService.CurrentCourse.Assignments.Add(Assignment);
        }
    }
}
