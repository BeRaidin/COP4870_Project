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
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation.Collections;
using Windows.Services.Maps;

namespace UWP.LearningManagement.ViewModels
{
    public class AssignmentViewModel
    {
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;
        private readonly PersonService personService;

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
        public Module SelectedModule
        {
            get { return moduleService.CurrentModule; }
            set { moduleService.CurrentModule = value; }
        }

        private Assignment _assignment;
        public Assignment Assignment
        {
            get { return _assignment; }
            set { _assignment = value; }
        }
        public AssignmentItem AssignmentItem { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TotalPoints { get; set; }
        public string Group { get; set; }
        public DateTimeOffset DueDate { get; set; }
        private List<AssignmentGroup> _assignmentGroups;
        public ObservableCollection<string> AssignmentGroups
        { get; set; }
        public string SelectedGroup { get; set; }
        public string SelectedModuleName { get; set; }
        public string GroupName { get; set; }
        public string Weight { get; set; }
        private List<Module> _modules;
        public ObservableCollection<string> Modules { get; set; }

        public AssignmentViewModel()
        {
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            personService = PersonService.Current;

            if (SelectedItem == null)
            {
                SelectedItem = new AssignmentItem();
            }
            AssignmentItem = SelectedItem as AssignmentItem;
            Assignment = new Assignment();
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

            if (SelectedItem == null)
            {
                SelectedItem = new AssignmentItem();
            }
            AssignmentItem = SelectedItem as AssignmentItem;
            Assignment = assignment;
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
            Assignment.DueDate = DueDate;
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

            if (SelectedModuleName != null && SelectedModuleName != "Make new Module")
            {
                foreach (var module in SelectedCourse.Modules)
                {
                    if (module.Name.Equals(SelectedModuleName))
                    {
                        AssignmentItem = new AssignmentItem();
                        AssignmentItem.Name = Assignment.Name;
                        AssignmentItem.Description = Assignment.Description;
                        AssignmentItem.Assignment = Assignment;
                        module.Content.Add(AssignmentItem);
                        SelectedModule = module;
                    }
                }

            }
            else if ((SelectedModuleName == null || SelectedModuleName == "Make new Module") && AssignmentItem != null)            
            {
                AssignmentItem.Assignment = Assignment;
            }

            if (SelectedModuleName == null || SelectedModuleName == "Make new Module")
            {
                var tempModule = new Module { Name= "Make new Module" };
                SelectedModule = tempModule;
                SelectedModule.Content.Add(AssignmentItem);
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
            SelectedModule = null;
        }

        public void Edit()
        {
            personService.CurrentAssignment.Name = Name;
            personService.CurrentAssignment.Description = Description;
            personService.CurrentAssignment.DueDate = DueDate;
            if (int.TryParse(TotalPoints, out var points))
            {
                personService.CurrentAssignment.TotalAvailablePoints = points;
            }
        }
    }
}
