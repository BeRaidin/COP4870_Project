using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.Models;

namespace UWP.LearningManagement.ViewModels
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public ObservableCollection<string> StudentLevels
        {
            get { return new ObservableCollection<string> { "Freshman", "Sophmore", "Junior", "Senior" }; }
        }
        public string SelectedClass { get; set; }

        public string Display
        {
            get { return $"[{Student.Id}] {Student.FirstName} {Student.LastName} - {Student.Classification}"; }
        }

        public StudentViewModel(Student student)
        {
            Student = student;
        }

        public StudentViewModel()
        {
            Student = new Student { Id = "-1"};

        }

        public async Task<Student> Addstudent()
        {
            if (SelectedClass == null || SelectedClass == "" || SelectedClass == "Freshman")
            {
                Student.Classification = Student.Classes.Freshman;
            }
            else if (SelectedClass == "Sophmore")
            {
                Student.Classification = Student.Classes.Sophmore;
            }
            else if (SelectedClass == "Junior")
            {
                Student.Classification = Student.Classes.Junior;
            }
            else Student.Classification = Student.Classes.Senior;

            var handler = new WebRequestHandler();
            var returnVal = await handler.Post("http://localhost:5159/Student", Student);
            var deserializedReturn = JsonConvert.DeserializeObject<Student>(returnVal);
            return deserializedReturn;
        }
        
    }
}
