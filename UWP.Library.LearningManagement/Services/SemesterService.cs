using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class SemesterService
    {
        private List<Semester> semesterList;
        public List<Semester> SemesterList
        {
            get
            {
                return semesterList;
            }
            set
            {
                semesterList = value;
            }
        }
        private Semester _currentSemester;
        public Semester CurrentSemester
        {
            get { return _currentSemester; }
            set { _currentSemester = value; }
        }
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
            SemesterList = new List<Semester>();
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
            foreach(var semester in SemesterList)
            {
                semester.Remove(removedCourse);
            }
        }
    }
}
