using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class PreviousSemesterViewModel
    {
        private readonly SemesterService semesterService;

        private Semester CurrentSemester
        {
            get { return  semesterService.CurrentSemester;}
        }

        private List<Semester> SemesterList 
        { get { return semesterService.SemesterList; }}
        public ObservableCollection<Semester> PreviousSemesters { get; set; }

        public PreviousSemesterViewModel()
        {
            semesterService = SemesterService.Current;
            PreviousSemesters = new ObservableCollection<Semester>();
            foreach(var semester in SemesterList)
            {
                if(semester.Year != CurrentSemester.Year || semester.Period != CurrentSemester.Period)
                {
                    PreviousSemesters.Add(semester);
                }
            }
        }

    }
}
