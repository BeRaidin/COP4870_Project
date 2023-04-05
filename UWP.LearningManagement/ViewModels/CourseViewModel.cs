using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation.Collections;

namespace UWP.LearningManagement.ViewModels
{
    public class CourseViewModel
    {
        private readonly CourseService courseService;
        private readonly PersonService personService;
        private readonly SemesterService semesterService;

        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }

        private List<Person> allInstructors;
        private ObservableCollection<Person> _instructors;
        public ObservableCollection<Person> Instructors
        {
            get { return _instructors; }
            set { _instructors = value; }
        }
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

        public CourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            allInstructors = new List<Person>();
            foreach (var person in semesterService.CurrentSemester.People)
            {
                if (person as Student == null)
                {
                    allInstructors.Add(person);
                }
            }
            Instructors = new ObservableCollection<Person>(allInstructors);
            FillChecks();
        }

        public void Set()
        {
            foreach (Person person in Instructors)
            {
                if(person.IsSelected)
                {
                    Roster.Add(person);
                    person.Add(SelectedCourse);
                    if(person as Student != null)
                    {
                        (person as Student).FinalGrades.Add(SelectedCourse, 0);
                    }
                }
            }

            if(int.TryParse(Hours, out int hours))
            {
                SelectedCourse.CreditHours = hours;
            }
            else
            {
                SelectedCourse.CreditHours = 3;
            }
        }

        public void Add()
        {
            bool test = true;
            foreach (var course in semesterService.CurrentSemester.Courses)
            {
                if (course.Code.Equals(Code, StringComparison.InvariantCultureIgnoreCase)
                    || course.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    test = false;
                }
            }


            if (Code != null && Code != "" && Name != null && Name != "" && Room != null && Room != "" && test)
            {
                Set();
                courseService.Add(SelectedCourse);
                semesterService.CurrentSemester.Courses.Add(SelectedCourse);
            }
            SelectedCourse = null;
        }

        public void Edit()
        {
            if (Code != null && Code != "" && Name != null && Name != "" && Room != null && Room != "")
            {
                SelectedCourse.Name = Name;
                SelectedCourse.Code = Code;
                foreach (var instructor in Instructors)
                {
                    if (instructor.IsSelected && !SelectedCourse.Roster.Contains(instructor))
                    {
                        SelectedCourse.Add(instructor);
                        instructor.Add(SelectedCourse);
                    }
                    else if (!instructor.IsSelected && SelectedCourse.Roster.Contains(instructor))
                    {
                        SelectedCourse.Remove(instructor);
                        instructor.Remove(SelectedCourse);
                    }
                }
            }
            else
            {
                GetTemp();
            }
            SelectedCourse = null;
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

        public void FillChecks()
        {
            foreach(var instructor in Instructors)
            {
                if(SelectedCourse.Roster.Contains(instructor))
                {
                    instructor.IsSelected = true;
                }
                else
                {
                    instructor.IsSelected = false;
                }
            }
        }
    }
}
