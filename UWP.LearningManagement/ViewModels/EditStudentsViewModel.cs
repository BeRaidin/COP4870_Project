using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.Library.LearningManagement.Database;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class EditStudentsViewModel
    {
        private IEnumerable<StudentViewModel> AllStudents
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Student").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Student>>(payload).Select(d => new StudentViewModel(d));
                return returnVal;
            }
        }
        public ObservableCollection<StudentViewModel> Students { get; set; }
        public StudentViewModel SelectedStudent { get; set; }

        public string Query { get; set; }

        public EditStudentsViewModel()
        {
            Students = new ObservableCollection<StudentViewModel>(AllStudents);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {
                IEnumerable<StudentViewModel> searchResults = AllStudents.Where(i => i.Student.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                    || i.Student.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));

                Students.Clear();
                foreach (var person in searchResults)
                {
                    Students.Add(person);
                }
            }
            else
            {
                Refresh();
            }
        }

        public async Task<Student> Delete()
        {
            string returnVal = await new WebRequestHandler().Post("http://localhost:5159/Student/Delete", SelectedStudent.Student);
            var deserializedReturn = JsonConvert.DeserializeObject<Student>(returnVal);
            return deserializedReturn;
        }

        public void Refresh()
        {
            Students.Clear();
            foreach (var student in AllStudents)
            {
                Students.Add(student);
            }
        }
    }
}
