using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class SemesterEC
    {
        public List<Semester> GetSemesters()
        {
            return FakeDataBase.Semesters;
        }

        public List<Semester> GetCurrentSemester()
        {
            return FakeDataBase.CurrentSemester;
        }

        public Semester AddSemester(Semester semester)
        {
            if (!FakeDataBase.Semesters.Any(x => x.Id == semester.Id))
            {
                FakeDataBase.Semesters.Add(semester);
            }
            return semester;
        }

        public Semester SetCurrent(Semester semester)
        {
            FakeDataBase.Courses.Clear();
            FakeDataBase.Assignments.Clear();
            FakeDataBase.Announcements.Clear();
            FakeDataBase.Modules.Clear();
            FakeDataBase.People.Clear();
            FakeDataBase.ContentItems.Clear();
            FakeDataBase.CurrentSemester.Clear();

            foreach (var course in semester.Courses)
            {
                FakeDataBase.Courses.Add(course);
            }
            foreach (var ssignment in semester.Assignments)
            {
                FakeDataBase.Assignments.Add(ssignment);
            }
            foreach (var announcement in semester.Announcements)
            {
                FakeDataBase.Announcements.Add(announcement);
            }
            foreach (var module in semester.Modules)
            {
                FakeDataBase.Modules.Add(module);
            }
            foreach (var person in semester.People)
            {
                FakeDataBase.People.Add(person);
            }
            foreach (var contentItem in semester.ContentItems)
            {
                FakeDataBase.ContentItems.Add(contentItem);
            }

            FakeDataBase.CurrentSemester.Add(semester);
            return semester;
        }
    }
}
