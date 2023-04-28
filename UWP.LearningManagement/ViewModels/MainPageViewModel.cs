using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using UWP.LearningManagement.ViewModels;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using System.Linq;

namespace LearningManagement.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Semester Semester { get; set; }
        public string Period { get; set; }
        public int Year { get; set; }
        public int Id { get; set; }
        public int PreviousId { get; set; }

        public MainPageViewModel()
        {
            var payload = new WebRequestHandler().Get("http://localhost:5159/Semester/GetCurrentSemester").Result;
            Semester = JsonConvert.DeserializeObject<List<Semester>>(payload)[0];
            Period = Semester.Period;
            Year = Semester.Year;
            Id = Semester.Id;
        }

        public async Task LeftClick()
        {
            await SetSemester();
            PreviousId = Id;
            Id--;
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
            await SetSelectedSemester();
        }

        public async Task RightClick()
        {
            await SetSemester();
            PreviousId = Id;
            Id++;
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
            await SetSelectedSemester();
        }

        public async Task SetSelectedSemester()
        {
            SemesterViewModel semesterView = new SemesterViewModel(Id);
            if (semesterView.Semester == null)
            {
                Semester = new Semester { Period = Period, Year = Year, Id = Id };
                Semester previousSemester = new SemesterViewModel(PreviousId).Semester;
                Semester.SetSemester(previousSemester.Courses, previousSemester.People);
            }
            else
            {
                Semester = semesterView.Semester;
            }
            await SetSemester();
            OnPropertyChanged(nameof(Semester));
        }

        public async Task SetSemester()
        {
            SemesterViewModel semesterView = new SemesterViewModel(Semester.Id);
            if (semesterView.Semester == null)
            {
                await new WebRequestHandler().Post("http://localhost:5159/Semester/Add", Semester);
            }
            await new WebRequestHandler().Post("http://localhost:5159/Semester/SetCurrent", Semester);
        }
    }
}
