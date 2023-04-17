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
            FakeDataBase.Students.Add(new Student(p));
            return p;
        }
    }
}
