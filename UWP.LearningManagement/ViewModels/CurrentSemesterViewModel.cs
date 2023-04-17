using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;

namespace UWP.LearningManagement.ViewModels
{
    public class CurrentSemesterViewModel
    {
        private readonly PersonService personService;

        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public Student Student { get; set; }
        public Dictionary<Course, double> FinalGrades
        {
            get { return Student.FinalGrades; }
        }

        public CurrentSemesterViewModel()
        {
            personService = PersonService.Current;
            Student = SelectedPerson as Student;
        }
    }
}
