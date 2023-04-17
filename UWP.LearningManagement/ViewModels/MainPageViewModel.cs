using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public Semester SelectedSemester 
        {
            get { return semesterService.CurrentSemester; }
            set { semesterService.CurrentSemester = value; } 
        }
        public List<Semester> SemesterList
        {
            get { return semesterService.SemesterList; }
        }
        public string Period { get; set; }
        public int Year { get; set; }

        public MainPageViewModel() 
        { 
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;

            if (SemesterList.Count == 0)
            {
                SelectedSemester = new Semester { Period = "Spring", Year = 2023 };
                SelectedSemester.SetSemester(courseService.Courses, personService.People);
                semesterService.Add(SelectedSemester);
            }
            Period = SelectedSemester.Period;
            Year = SelectedSemester.Year;
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
            if (Period.Equals("Spring"))
            {
                Period = "Fall";
                Year--;
            }
            else if (Period.Equals("Summer"))
            {
                Period = "Spring";
            }
            else if (Period.Equals("Fall"))
            {
                Period = "Summer";
            }
            SetSelectedSemester();
        }

        public void RightClick()
        {
            if (Period.Equals("Fall"))
            {
                Period = "Spring";
                Year++;
            }
            else if (Period.Equals("Summer"))
            {
                Period = "Fall";
            }
            else if (Period.Equals("Spring"))
            {
                Period = "Summer";
            }
            SetSelectedSemester();
        }

        public void SetSelectedSemester()
        {
            bool isNewSemester = true;
            foreach (var semester in SemesterList)
            {
                if (semester.Year == Year && semester.Period == Period)
                {
                    SelectedSemester = semester;
                    isNewSemester = false;
                    break;
                }
            }

            if (isNewSemester)
            {
                SelectedSemester = new Semester { Period = Period, Year = Year };
                SelectedSemester.SetSemester(courseService.Courses, personService.People);
                semesterService.Add(SelectedSemester);
            }

            RaisePropertyChanged("Period");
            RaisePropertyChanged("Year");
        }
    }
}
