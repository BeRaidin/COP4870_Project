using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using System.Threading.Tasks;
using UWP.Library.LearningManagement.Database;
using System.Linq;
using Windows.Services.Maps;

namespace UWP.LearningManagement.ViewModels
{
    public class CourseViewModel
    {
        public virtual string Display => $"[{Course.Code}] - {Course.Name}";


        private List<Instructor> InstructorList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetInstructors").Result;
                return JsonConvert.DeserializeObject<List<Instructor>>(payload).ToList();
            }
        }
        private List<TeachingAssistant> AssistantList

        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetAssistants").Result;
                return JsonConvert.DeserializeObject<List<TeachingAssistant>>(payload).ToList();
            }
        }
        private List<Student> StudentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                return JsonConvert.DeserializeObject<List<Student>>(payload).ToList();
            }
        }
        private List<Course> CourseList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload).ToList();
            }
        }

        public Course Course { get; set; }
        public Person Person { get; set; }
        
        public List<Person> Roster
        {
            get
            {
                return Course.Roster;
            }
            set
            {
                Course.Roster = value;
            }
        }
        public string Hours { get; set; }
        public bool IsValid;

        public CourseViewModel(int id, int courseId = -1)
        {
            if (id < 0)
            {
                Person = new Person { Id = -1 };
            }
            else
            {
                Person = InstructorList.FirstOrDefault(x => x.Id == id);
                if (Person == null)
                {
                    Person = AssistantList.FirstOrDefault(x => x.Id == id);
                }
            }

            if (courseId == -1)
            {
                Course = new Course { Id = -1 };
            }
            else
            {
                Course = CourseList.FirstOrDefault(x => x.Id == courseId);
            }
            IsValid = true;
        }

        public CourseViewModel() { }

        

        public async Task<Course> AddCourse()
        {
            if (int.TryParse(Hours, out var totalHours))
            {
                Course.CreditHours = totalHours;
            }
            else
            {
                Course.CreditHours = 3;
            }

            if (Course.Id != -1)
            {
                foreach(var person in Course.Roster)
                {
                    Person editedPerson = InstructorList.FirstOrDefault(x =>x.Id == person.Id);
                    if(editedPerson == null)
                    {
                        editedPerson = AssistantList.FirstOrDefault(x => x.Id == person.Id);
                        if(editedPerson == null)
                        {
                            editedPerson = StudentList.FirstOrDefault(x => x.Id == person.Id);
                        }
                    }
                    if(editedPerson is Student student)
                    {
                        FinalGradesDictionary courseGrade = student.FinalGrades.FirstOrDefault(x => x.Key.Id == Course.Id);
                        if (courseGrade != null)
                        {
                            double grade = courseGrade.Value;
                            student.FinalGrades.Remove(courseGrade);
                            student.FinalGrades.Add(new FinalGradesDictionary(Course, grade));
                            await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateStudentCourses", student);
                        }
                    }
                    else
                    {
                        editedPerson.Add(Course);
                        await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", editedPerson);
                    }
                }
                var returnVal = await new WebRequestHandler().Post("http://localhost:5159/Course/AddOrUpdate", Course);
                return JsonConvert.DeserializeObject<Course>(returnVal);

            }
            else
            {
                Course.Add(Person);
                var returnVal = await new WebRequestHandler().Post("http://localhost:5159/Course/AddOrUpdate", Course);
                Course deserializedReturn = JsonConvert.DeserializeObject<Course>(returnVal);
                Person.Add(deserializedReturn);
                await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", Person);
                return deserializedReturn;
            }
        }

        public void Edit()
        {
            //            if (Code != null && Code != "" && Name != null && Name != "" && Room != null && Room != "")
            //            {
            //                SelectedCourse.Name = Name;
            //                SelectedCourse.Code = Code;
            //                foreach (var instructor in Instructors)
            //                {
            //                    if (instructor.IsSelected && !SelectedCourse.Roster.Contains(instructor))
            //                    {
            //                        SelectedCourse.Add(instructor);
            //                        instructor.Add(SelectedCourse);
            //                    }
            //                    else if (!instructor.IsSelected && SelectedCourse.Roster.Contains(instructor))
            //                    {
            //                        SelectedCourse.Remove(instructor);
            //                        instructor.Remove(SelectedCourse);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                GetTemp();
            //            }
            //            SelectedCourse = null;
        }
    }
}
