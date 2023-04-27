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

        public ModuleViewModel SelectedModule { get; set; }
        public Assignment Assignment { get; set; }
        public Course Course { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public ObservableCollection<AssignmentGroup> AssignmentGroups { get; set; }
        public AssignmentGroup SelectedGroup { get; set; }
        public string GroupName { get; set; }
        public string Weight { get; set; }
        public ObservableCollection<ModuleViewModel> Modules { get; set; }
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
                AssignmentGroups = new ObservableCollection<AssignmentGroup>();
                Modules = new ObservableCollection<ModuleViewModel>();
                AssignmentGroups = new ObservableCollection<AssignmentGroup>();
                foreach (var module in Course.Modules)
                {
                    Modules.Add(new ModuleViewModel(module.Id));
                }
                foreach (var group in Course.AssignmentGroups)
                {
                    AssignmentGroups.Add(group);
                }
                AssignmentGroups.Add(new AssignmentGroup { Name = "Make a new Assignment Group" });
            }
        }

        public AssignmentViewModel() { }

        public async Task<Assignment> Add()
        {
            if (SelectedModule != null)
            {
                Assignment.DueDate = DueDate;
                if (SelectedGroup != null && SelectedGroup.Name != "Make a new Assignment Group")
                {
                    Assignment.AssignmentGroup = SelectedGroup;
                }

                await SelectedModule.Add(Assignment);

                if (int.TryParse(TotalPoints, out var totalPoints))
                {
                    Assignment.TotalAvailablePoints = totalPoints;
                }
                else
                {
                    Assignment.TotalAvailablePoints = 100;
                }
                var returnVal = await new WebRequestHandler().Post("http://localhost:5159/Assignment/AddOrUpdate", Assignment);
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
            Assignment = new Assignment { Name = "Invalid" };
            return Assignment;
        }

        public bool NeedsGroup()
        {
            if ((SelectedGroup == null || SelectedGroup.Name == "Make a new Assignment Group") && Assignment.Name != "Invalid" && Assignment.Name != "")
            {
                return true;
            }
            return false;
        }

        public async Task MakeNewAssignGroup()
        {
            AssignmentGroup newGroup;
            if (int.TryParse(Weight, out var percent))
            {
                newGroup = new AssignmentGroup { Name = GroupName, Weight = percent };
            }
            else
            {
                newGroup = new AssignmentGroup { Name = GroupName, Weight = 20 };
            }
            Course.AssignmentGroups.Add(newGroup);
            await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateAssignGroups", Course);
            Assignment.AssignmentGroup = newGroup;
            await new WebRequestHandler().Post("http://localhost:5159/Assignment/UpdateAssignGroup", Assignment);
        }
    }
}
