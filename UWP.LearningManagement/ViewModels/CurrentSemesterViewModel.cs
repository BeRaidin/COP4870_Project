using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using System.Linq;

namespace UWP.LearningManagement.ViewModels
{
    public class CurrentSemesterViewModel
    {
        private IEnumerable<Student> AllStudents
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                return JsonConvert.DeserializeObject<List<Student>>(payload);
            }
        }
        public Person SelectedPerson { get; set; }
        public Student Student { get; set; }
        public List<FinalGradesDictionary> FinalGrades
        {
            get { return Student.FinalGrades; }
        }

        public CurrentSemesterViewModel(int id)
        {
            Student = AllStudents.FirstOrDefault(x => x.Id == id);
        }
    }
}
