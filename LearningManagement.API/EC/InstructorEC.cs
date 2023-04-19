using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class InstructorEC
    {
        public List<Instructor> GetInstructors()
        {
            return FakeDataBase.Instructors;
        }

        public List<TeachingAssistant> GetAssistants()
        {
            return FakeDataBase.Assistants;
        }

        public Person AddorUpdateAdmin(Person p)
        {
            if (int.Parse(p.Id) <= 0)
            {
                var lastId = FakeDataBase.People.Select(p => int.Parse(p.Id)).Max();
                p.Id = lastId++.ToString();
            }

            if (p is Instructor instructor)
            {
                FakeDataBase.People.Add(instructor);
            }
            else if( p is TeachingAssistant assistant)
            {
                FakeDataBase.People.Add(assistant);
            }

            return p;
        }
    }
}
