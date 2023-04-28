using UWP.Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.Library.LearningManagement.Database;

namespace Library.LearningManagement.Services
{
    public class SemesterService
    {
        public List<Semester> SemesterList
        {
            get
            {
                return FakeDataBase.Semesters;
            }
        }
        public Semester CurrentSemester { get; set; }
        public static SemesterService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new SemesterService();
                }

                return instance;
            }
        }
        private static SemesterService instance;

        public SemesterService()
        {
            CurrentSemester = new Semester();
        }

        public void Add(Semester semester)
        {
            SemesterList.Add(semester);
        }

        public void Remove(Person removedPerson)
        {
            foreach (var semester in SemesterList)
            {
                semester.Remove(removedPerson);
            }
        }

        public void Remove(Course removedCourse)
        {
            foreach (var semester in SemesterList)
            {
                semester.Remove(removedCourse);
            }
        }
    }
}
