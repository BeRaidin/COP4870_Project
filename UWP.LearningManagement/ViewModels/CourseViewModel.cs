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



        public string Name
        {
            get
            {
                return Course.Name;
            }
            set
            {
                Course.Name = value;
            }
        }
        public string Code
        {
            get
            {
                return Course.Code;
            }
            set
            {
                Course.Code = value;
            }
        }
        public string Room
        {
            get { return Course.Room; }
            set { Course.Room = value; }
        }
        public string TempName { get; set; }
        public string TempCode { get; set; }
        public string TempRoom { get; set; }
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

        public void Set()
        {
            if (int.TryParse(Hours, out int hours))
            {
                Course.CreditHours = hours;
            }
            else
            {
                Course.CreditHours = 3;
            }

            //Course.Roster.Add(SelectedPerson);
        }

        public async Task<Course> AddCourse()
        {
            Course.Add(Person);
            var returnVal = await new WebRequestHandler().Post("http://localhost:5159/Course/AddOrUpdate", Course);
            Course deserializedReturn = JsonConvert.DeserializeObject<Course>(returnVal);

            Person.Add(deserializedReturn);
            await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", Person);

            return deserializedReturn;
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
        public void SetTemp()
        {
            TempName = Name.ToString();
            TempCode = Code.ToString();
            TempRoom = Room.ToString();
        }
        public void GetTemp()
        {
            Name = TempName;
            Code = TempCode;
            Room = TempRoom;
        }
    }
}
