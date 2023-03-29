using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.Dialogs;
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
        public AssignmentItem AssignmentItem { get; set; }
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
        private List<AssignmentGroup> _assignmentGroups;
        public ObservableCollection<string> AssignmentGroups
        { get; set; }
        public string SelectedGroup { get; set; }
        public string GroupName { get; set; }
        public string Weight { get; set; }

        public AssignmentViewModel()
        {
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            AssignmentItem = moduleService.CurrentItem as AssignmentItem;
            Assignment = new Assignment();
            DueDate = DateTimeOffset.Now;
            _assignmentGroups = courseService.CurrentCourse.AssignmentGroups;
            AssignmentGroups = new ObservableCollection<string>();
            foreach(var group in _assignmentGroups)
            {
                AssignmentGroups.Add(group.Name);
            }
            AssignmentGroups.Add("Make new Assignment Group");
        }

        public AssignmentViewModel(Assignment assignment)
        {
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            AssignmentItem = moduleService.CurrentItem as AssignmentItem;
            Assignment = assignment;
            DueDate = DateTimeOffset.Now;
            _assignmentGroups = courseService.CurrentCourse.AssignmentGroups;
            AssignmentGroups = new ObservableCollection<string>();
            foreach (var group in _assignmentGroups)
            {
                AssignmentGroups.Add(group.Name);
            }
            AssignmentGroups.Add("Make new Assignment Group");
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
            Assignment.AssignmentGroup = _assignmentGroups.FirstOrDefault(i => i.Name.Equals(SelectedGroup));
            if (AssignmentItem != null)
            {
                AssignmentItem.Assignment = Assignment;
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

        public void MakeNewAssignGroup()
        {
            var assignmentGroup = new AssignmentGroup();
            assignmentGroup.Name = GroupName;
            if (int.TryParse(Weight, out var weight))
            {
                assignmentGroup.Weight = weight;
            }
            else { assignmentGroup.Weight = 20; }
           Assignment.AssignmentGroup = assignmentGroup;
            courseService.CurrentCourse.AssignmentGroups.Add(assignmentGroup);
        }
    }
}
