using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UWP.LearningManagement.Dialogs;
using Windows.Graphics;

namespace UWP.LearningManagement.ViewModels
{
    public class PreviousSemesterViewModel
    {
        private readonly SemesterService semesterService;
        private readonly PersonService personService;

        private Semester CurrentSemester
        {
            get { return  semesterService.CurrentSemester;}
        }
        private List<Semester> SemesterList 
        { 
            get { return semesterService.SemesterList; }
        }
        public List<FinalGradesDictionary> Courses { get; set; }

        public ObservableCollection<Semester> PreviousSemesters { get; set; }
        public Semester Semester { get; set; }
        public string SemesterTitle { get; set; }
        public PreviousSemesterViewModel()
        {
            semesterService = SemesterService.Current;
            personService = PersonService.Current;
            PreviousSemesters = new ObservableCollection<Semester>();
            foreach(var semester in SemesterList)
            {
                if(semester.Year != CurrentSemester.Year || semester.Period != CurrentSemester.Period)
                {
                    PreviousSemesters.Add(semester);
                }
            }
        }
        public PreviousSemesterViewModel(Semester semester)
        {
            personService = PersonService.Current;
            Semester = semester;
            SemesterTitle = Semester.Display;
            foreach(var person in Semester.People)
            {
                if(personService.CurrentPerson.Display == person.Display) 
                {
                    Courses = (person as Student).FinalGrades;
                }
            }    

        }

        public async void ViewPastSemester()
        {
            if (Semester != null)
            {
                var dialog = new PreviousSemesterDialog(Semester);
                if (dialog != null)
                {
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
