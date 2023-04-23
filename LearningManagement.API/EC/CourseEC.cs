using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class CourseEC
    {
        public List<Course> GetCourses()
        {
            return FakeDataBase.Courses;
        }

        public Course AddOrUpdateCourse(Course c)
        {
            bool isNew = false;
            if (FakeDataBase.Courses.Count == 0)
            {
                c.Id = 0;
                isNew = true;
            }
            else if (c.Id < 0)
            {
                var lastId = FakeDataBase.Courses.Select(p => p.Id).Max();
                lastId++;
                c.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.Courses.Add(c);
            }
            else
            {
                var editedCourse = FakeDataBase.Courses.FirstOrDefault(p => p.Id == c.Id);
                if (editedCourse != null)
                {
                    editedCourse.Code = c.Code;
                    editedCourse.Name = c.Name;
                    editedCourse.Room = c.Room;
                    editedCourse.CreditHours = c.CreditHours;
                }
            }
            return c;
        }
    }
}
