﻿using UWP.Library.LearningManagement.Models;
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
        private readonly CourseService courseService;
        private readonly PersonService personService;

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
        public Course Course { get; set; }
        public Person Person { get; set; }

        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public Person SelectedPerson
        { get { return personService.CurrentPerson; } } 
        public string Name
        {
            get
            {
                return SelectedCourse.Name;
            }
            set
            {
                SelectedCourse.Name = value;
            }
        }
        public string Code
        {
            get
            {
                return SelectedCourse.Code;
            }
            set
            {
                SelectedCourse.Code = value;
            }
        }
        public string Room
        {
            get { return SelectedCourse.Room; }
            set { SelectedCourse.Room = value; }
        }
        public string TempName { get; set; }
        public string TempCode { get; set; }
        public string TempRoom { get; set; }
        public List<Person> Roster 
        {
            get
            {
                return SelectedCourse.Roster;
            }
            set
            {
                SelectedCourse.Roster = value; 
            }
        }
        public string Hours { get; set; }
        public bool IsValid;

        public CourseViewModel(int id, Course course = null)
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            if(id < 0)
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

            if(course == null)
            {
                Course = new Course { Id = -1 };
            }
            else
            {
                Course = course;
            }
            IsValid = true;
        }

        public CourseViewModel() { }

        public void Set()
        {
            if(int.TryParse(Hours, out int hours))
            {
                SelectedCourse.CreditHours = hours;
            }
            else
            {
                SelectedCourse.CreditHours = 3;
            }

            SelectedCourse.Roster.Add(SelectedPerson);
        }

        public async Task<Course> AddCourse()
        {

            if(Person != null)
            { 
                Person.Add(Course);
                await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", Person);
            }


            var handler = new WebRequestHandler();
            string returnVal;


            Course deserializedReturn;

            returnVal = await handler.Post("http://localhost:5159/Course/AddOrUpdate", Course);
            deserializedReturn = JsonConvert.DeserializeObject<Course>(returnVal);

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
