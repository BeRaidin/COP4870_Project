using Library.LearningManagement.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.Models;

namespace UWP.LearningManagement.ViewModels
{
    public class AssignmentViewModel
    {

        public IEnumerable<Assignment> AssignmentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Assignment").Result;
                return JsonConvert.DeserializeObject<List<Assignment>>(payload);
            }
        }
        public IEnumerable<Course> CourseList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload);
            }
        }
        public IEnumerable<Student> StudentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                return JsonConvert.DeserializeObject<List<Student>>(payload);
            }
        }
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;

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
                Assignment = AssignmentList.FirstOrDefault(x => x.Id == id);
                DueDate = Assignment.DueDate;
                TotalPoints = Assignment.TotalAvailablePoints.ToString();

            }
            else
            {
                Assignment = new Assignment { Id = -1 };
                DueDate = DateTimeOffset.Now;
            }
            if (courseId != -1)
            {
                Course = CourseList.FirstOrDefault(x => x.Id == courseId);
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

        public async Task<Assignment> Add()
        {
            if (!Course.Assignments.Any(x => x.Name == Assignment.Name))
            {
                Assignment.DueDate = DueDate;
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
                Assignment editedAssignment = AssignmentList.FirstOrDefault(x => x.Id == deserializedReturn.Id);
                Course.Add(deserializedReturn);
                foreach (var person in Course.Roster)
                {
                    Student student = StudentList.FirstOrDefault(x => x.Id == person.Id);
                    if (student != null)
                    {
                        double score = 0;
                        var oldGrade = student.Grades.FirstOrDefault(x => x.Assignment.Id == Assignment.Id);
                        if (oldGrade != null)
                        {
                            score = oldGrade.Grade;
                            student.Grades.Remove(oldGrade);
                        }
                        student.Grades.Add(new GradesDictionary
                        { Assignment = editedAssignment, Grade = score, CourseCode = Course.Code, PersonName = student.FirstName + " " + student.LastName });
                        
                        await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateStudentCourses", student);
                    }
                }

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
