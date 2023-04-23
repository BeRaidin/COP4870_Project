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
        private readonly CourseService courseService;
        private readonly PersonService personService;

        public InstructorDetailsViewModel ParentViewModel { get; set; }

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

        public CourseViewModel(InstructorDetailsViewModel idvm)
        {
            ParentViewModel = idvm;
            courseService = CourseService.Current;
            personService = PersonService.Current;
            if (ParentViewModel?.SelectedCourse?.Course == null)
            {
                Course = new Course { Id = -1 };
            }
            else Course = ParentViewModel.SelectedCourse.Course;
            if (ParentViewModel?.SelectedPerson == null)
            {
                Person = new Person { Id = -1 };
            }
            else Person = ParentViewModel.SelectedPerson;

            IsValid = true;
        }

        public CourseViewModel(InstructorDetailsViewModel idvm, Course course)
        {
            ParentViewModel = idvm;
            courseService = CourseService.Current;
            personService = PersonService.Current;
            Course = course;
            Person = new Person { Id = -1 };
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
                var person = FakeDataBase.People.FirstOrDefault(p => p.Id == SelectedPerson.Id );
                person?.Add(Course);
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
