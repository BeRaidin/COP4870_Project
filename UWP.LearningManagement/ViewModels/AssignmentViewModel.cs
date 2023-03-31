using Library.LearningManagement.Model;
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
using Windows.Services.Maps;

namespace UWP.LearningManagement.ViewModels
{
    public class AssignmentViewModel
    {
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;

        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value;}
        }
        public ContentItem SelectedItem
        {
            get { return moduleService.CurrentItem; }
            set { moduleService.CurrentItem = value; }
        }


        private Assignment _assignment;
        public Assignment Assignment
        {
            get { return _assignment; }
            set { _assignment = value; }
        }
        public AssignmentItem AssignmentItem { get; set; }
        public string Name
        {
            get { return SelectedItem.Name; }
            set { SelectedItem.Name = value; }
        }
        public string Description
        {
            get { return SelectedItem.Description; }
            set { SelectedItem.Description = value; }
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
        public string SelectedModule { get; set; }
        public string GroupName { get; set; }
        public string Weight { get; set; }
        private List<Module> _modules;
        public ObservableCollection<string> Modules { get; set; }

        public AssignmentViewModel()
        {
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            AssignmentItem = SelectedItem as AssignmentItem;
            Assignment = new Assignment();
            if(SelectedItem == null)
            {
                SelectedItem = new ContentItem();
            }
            DueDate = DateTimeOffset.Now;
            _modules = SelectedCourse.Modules;
            Modules = new ObservableCollection<string>();
            foreach (var module in _modules)
            {
                Modules.Add(module.Name);
            }
            Modules.Add("Make new Module");
            _assignmentGroups = SelectedCourse.AssignmentGroups;
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
            AssignmentItem = SelectedItem as AssignmentItem;
            Assignment = assignment;
            if (SelectedItem == null)
            {
                SelectedItem = new ContentItem();
            }
            DueDate = DateTimeOffset.Now;
            _assignmentGroups = SelectedCourse.AssignmentGroups;
            AssignmentGroups = new ObservableCollection<string>();
            foreach (var group in _assignmentGroups)
            {
                AssignmentGroups.Add(group.Name);
            }
            AssignmentGroups.Add("Make new Assignment Group");
            _modules = SelectedCourse.Modules;
            Modules = new ObservableCollection<string>();
            foreach (var module in _modules)
            {
                Modules.Add(module.Name);
            }
            Modules.Add("Make new Module");
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

            if (SelectedModule != null)
            {
                foreach(var module in SelectedCourse.Modules)
                {
                    if (module.Name.Equals(SelectedModule))
                    {
                        AssignmentItem = new AssignmentItem();
                        AssignmentItem.Name = Assignment.Name;
                        AssignmentItem.Description = Assignment.Description;
                        AssignmentItem.Assignment = Assignment;
                        module.Content.Add(AssignmentItem);
                    }
                }

            }
            else if (SelectedModule == null && AssignmentItem != null)            
            {
                AssignmentItem.Assignment = Assignment;
            }
            
            foreach (Person person in SelectedCourse.Roster)
            {
                var student = person as Student;
                if (student != null)
                {
                    var grade = new GradesDictionary { Assignment = Assignment, Grade = 0 };
                    student.Grades.Add(grade);
                }
            }
            SelectedCourse.Add(Assignment);
            SelectedItem = AssignmentItem;
        }

        public void MakeNewAssignGroup()
        {
            var assignmentGroup = new AssignmentGroup { Name = GroupName };
            if (int.TryParse(Weight, out var weight))
            {
                assignmentGroup.Weight = weight;
            }
            else { assignmentGroup.Weight = 20; }
            Assignment.AssignmentGroup = assignmentGroup;
            SelectedCourse.AssignmentGroups.Add(assignmentGroup);
        }

        public void ClearCurrent()
        {
            SelectedItem = null;
            moduleService.CurrentModule = null;
        }
    }
}
