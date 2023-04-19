using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class StudentEC
    {
        public List<Student> GetStudents()
        {
            return FakeDataBase.Students;
        }

        public Student AddorUpdateStudent(Student s)
        {
            if (int.Parse(s.Id) <= 0 && FakeDataBase.People.Count > 0)
            {
                var lastId = FakeDataBase.People.Select(p => int.Parse(p.Id)).Max();
                lastId++;
                s.Id = lastId.ToString();
            }
            else s.Id = "0";

            FakeDataBase.People.Add(s);
            return s;
        }
    }
}
