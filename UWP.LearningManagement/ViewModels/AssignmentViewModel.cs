using UWP.Library.LearningManagement.Models;
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
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;

namespace UWP.LearningManagement.ViewModels
{
    public class AssignmentViewModel
    {

        public IEnumerable<Assignment> Assignments
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Assignment").Result;
                return JsonConvert.DeserializeObject<List<Assignment>>(payload);
            }
        }
        public IEnumerable<Course> Courses
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload);
            }
        }
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;
        private readonly PersonService personService;

        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
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

        public Assignment Assignment { get; set; }
        public Course Course { get; set; }


        public AssignmentItem AssignmentItem { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public DateTimeOffset DueDate { get; set; }
        private readonly List<AssignmentGroup> assignmentGroups;
        public ObservableCollection<string> AssignmentGroups
        { get; set; }
        public string SelectedGroup { get; set; }
        public string SelectedModuleName { get; set; }
        public string GroupName { get; set; }
        public string Weight { get; set; }
        private readonly List<Module> modules;
        public ObservableCollection<string> Modules { get; set; }

        public bool IsValid;


        public string TotalPoints { get; set; }

        public virtual string Display => $"({Assignment.DueDate}) {Assignment.Name}";

        public AssignmentViewModel(int id, int courseId = -1)
        {
            if (id != -1)
            {
                Assignment = Assignments.FirstOrDefault(x => x.Id == id);
            }
            else Assignment = new Assignment { Id = -1 };
            if (courseId != -1)
            {
                Course = Courses.FirstOrDefault(x => x.Id == courseId);
            }




            //moduleService = ModuleService.Current;
            //courseService = CourseService.Current;
            //personService = PersonService.Current;
            //
            //if (SelectedItem == null)
            //{
            //    SelectedItem = new AssignmentItem();
            //}
            //AssignmentItem = SelectedItem as AssignmentItem;
            //Assignment = new Assignment();
            //DueDate = DateTimeOffset.Now;
            //modules = SelectedCourse.Modules;
            //Modules = new ObservableCollection<string>();
            //foreach (var module in modules)
            //{
            //    Modules.Add(module.Name);
            //}
            //Modules.Add("Make new Module");
            //assignmentGroups = SelectedCourse.AssignmentGroups;
            //AssignmentGroups = new ObservableCollection<string>();
            //foreach(var group in assignmentGroups)
            //{
            //    AssignmentGroups.Add(group.Name);
            //}
            //AssignmentGroups.Add("Make new Assignment Group");
            //IsValid = true;
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
            assignmentGroups = SelectedCourse.AssignmentGroups;
            AssignmentGroups = new ObservableCollection<string>();
            foreach (var group in assignmentGroups)
            {
                AssignmentGroups.Add(group.Name);
            }
            AssignmentGroups.Add("Make new Assignment Group");
            modules = SelectedCourse.Modules;
            Modules = new ObservableCollection<string>();
            foreach (var module in modules)
            {
                Modules.Add(module.Name);
            }
            Modules.Add("Make new Module");
            IsValid = true;
        }

        public void Set()
        {
            Assignment.Name = Name;
            Assignment.Description = Description;
            Assignment.DueDate = DueDate;
        }

        public async Task<Assignment> Add()
        {
            if (!Course.Assignments.Any(x => x.Name == Assignment.Name))
            {
                if (int.TryParse(TotalPoints, out var totalPoints))
                {
                    Assignment.TotalAvailablePoints = totalPoints;
                }
                else
                {
                    Assignment.TotalAvailablePoints = 100;
                }
                var handler = new WebRequestHandler();
                var returnVal = await handler.Post("http://localhost:5159/Assignment/AddOrUpdate", Assignment);
                var deserializedReturn = JsonConvert.DeserializeObject<Assignment>(returnVal);
                Course.Add(deserializedReturn);
                await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateAssignments", Course);
                return deserializedReturn;
            }
            return new Assignment { Name = "Invalid Name" };





            //if (Name == null || Name == "" || Description == null || Description == "")
            //{
            //    IsValid = false;
            //}
            //if (IsValid)
            //{
            //    foreach (var assignment in SelectedCourse.Assignments)
            //    {
            //        if (Name == assignment.Name)
            //        {
            //            IsValid = false;
            //            break;
            //        }
            //    }
            //}
            //if (IsValid)
            //{
            //    Set();
            //    Assignment.AssignmentGroup = assignmentGroups.FirstOrDefault(i => i.Name.Equals(SelectedGroup));
            //
            //    if (SelectedModuleName != null && SelectedModuleName != "Make new Module")
            //    {
            //        foreach (var module in SelectedCourse.Modules)
            //        {
            //            if (module.Name.Equals(SelectedModuleName))
            //            {
            //                AssignmentItem = new AssignmentItem
            //                {
            //                    Name = Assignment.Name,
            //                    Description = Assignment.Description,
            //                    Assignment = Assignment
            //                };
            //                module.Content.Add(AssignmentItem);
            //                SelectedModule = module;
            //            }
            //        }
            //
            //    }
            //    else if ((SelectedModuleName == null || SelectedModuleName == "Make new Module") && AssignmentItem != null)
            //    {
            //        AssignmentItem.Assignment = Assignment;
            //    }
            //
            //    if (SelectedModuleName == null || SelectedModuleName == "Make new Module")
            //    {
            //        var tempModule = new Module { Name = "Make new Module" };
            //        SelectedModule = tempModule;
            //        SelectedModule.Content.Add(AssignmentItem);
            //    }
            //
            //
            //    foreach (Person person in SelectedCourse.Roster)
            //    {
            //        if (person is Student student)
            //        {
            //            var grade = new GradesDictionary { Assignment = Assignment, Grade = 0, Course = SelectedCourse, Person = student };
            //            student.Grades.Add(grade);
            //        }
            //    }
            //    SelectedCourse.Add(Assignment);
            //    SelectedItem = AssignmentItem;
            //}
        }

        public void MakeNewAssignGroup()
        {
            if (GroupName != null && GroupName != "")
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
            else IsValid = false;
        }

        public void False()
        {
            IsValid = false;
        }
    }
}
