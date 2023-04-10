using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly PersonService personService;
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;
        private readonly SemesterService semesterService;

        public Semester CurrentSemester { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
        public bool isNewSemester;

        public MainPageViewModel() 
        { 
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;
            Semester = "Spring";
            Year = 2023;
            CurrentSemester = new Semester { Period=Semester, Year=Year};
            semesterService.CurrentSemester = CurrentSemester;
            semesterService.Add(CurrentSemester);
        }

        public void Clear()
        {
            personService.CurrentPerson = null;
            personService.CurrentAssignment = null;
            moduleService.CurrentModule = null;
            moduleService.CurrentItem = null;
            courseService.CurrentCourse = null;
        }

        public void LeftClick()
        {
            isNewSemester = true;
            if (Semester.Equals("Spring"))
            {
                Semester = "Fall";
                Year--;
            }
            else if (Semester.Equals("Summer"))
            {
                Semester = "Spring";
            }
            else if (Semester.Equals("Fall"))
            {
                Semester = "Summer";
            }

            foreach (var semester in semesterService.SemesterList)
            {
                if(semester.Year == Year && semester.Period == Semester)
                {
                    CurrentSemester = semester;
                    semesterService.CurrentSemester = semester;
                    isNewSemester = false;
                    break;
                }
            }

            if (isNewSemester)
            {
                var newSemester = new Semester { Period = Semester, Year = Year };
                newSemester.SetSemester(courseService.Courses, personService.People);
                semesterService.SemesterList.Add(newSemester);
                CurrentSemester = newSemester;
                semesterService.CurrentSemester = newSemester;
            }

            RaisePropertyChanged("Semester");
            RaisePropertyChanged("Year");

        }

        public void RightClick()
        {
            isNewSemester = true;
            if (Semester.Equals("Fall"))
            {
                Semester = "Spring";
                Year++;
            }
            else if (Semester.Equals("Summer"))
            {
                Semester = "Fall";
            }
            else if (Semester.Equals("Spring"))
            {
                Semester = "Summer";
            }

            foreach (var semester in semesterService.SemesterList)
            {
                if (semester.Year == Year && semester.Period == Semester)
                {
                    CurrentSemester = semester;
                    semesterService.CurrentSemester = semester;
                    isNewSemester = false;
                    break;
                }
            }

            if (isNewSemester)
            {
                var newSemester = new Semester { Period = Semester, Year = Year };
                newSemester.SetSemester(courseService.Courses, personService.People);
                semesterService.SemesterList.Add(newSemester);
                CurrentSemester = newSemester;
                semesterService.CurrentSemester = newSemester;
            }

            RaisePropertyChanged("Semester");
            RaisePropertyChanged("Year");
        }
    }
}
