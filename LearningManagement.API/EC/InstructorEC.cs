using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.DTO;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class InstructorEC
    {
        public List<InstructorDTO> GetInstructors()
        {
            return FakeDataBase.Instructors.Select(s => new InstructorDTO(s)).ToList();
        }

        public InstructorDTO AddorUpdateInstructor(InstructorDTO p)
        {
            if (int.Parse(p.Id) <= 0)
            {
                var lastId = FakeDataBase.People.Select(p => int.Parse(p.Id)).Max();
                p.Id = lastId++.ToString();
            }

            FakeDataBase.People.Add(new Instructor(p));
            return p;
        }
    }
}
