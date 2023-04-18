using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.DTO;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class StudentEC
    {
        public List<StudentDTO> GetStudents()
        {
            return FakeDataBase.Students.Select(s => new StudentDTO(s)).ToList();
        }

        public StudentDTO AddorUpdateStudent(StudentDTO p)
        {
            if(int.Parse(p.Id) <= 0)
            {
                var lastId = FakeDataBase.Students.Select(p => int.Parse(p.Id)).Max();
                p.Id = lastId++.ToString();
            }

            FakeDataBase.people.Add(new Student(p));
            return p;
        }
    }
}
