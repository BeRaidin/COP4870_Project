using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.DTO;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class InstructorEC
    {
        public List<InstructorDTO> GetInstructors()
        {
            var returnList = new List<InstructorDTO>();
            foreach(var person in FakeDataBase.Instructors)
            {
                returnList.Add(new InstructorDTO(person));
            }
            return returnList;
        }

        public List<TeachingAssistantDTO> GetAssistants()
        {
            var returnList = new List<TeachingAssistantDTO>();
            foreach (var person in FakeDataBase.Assistants)
            {
                returnList.Add(new TeachingAssistantDTO(person));
            }
            return returnList;
        }

        public PersonDTO AddorUpdateAdmin(PersonDTO p)
        {
            if (int.Parse(p.Id) <= 0)
            {
                var lastId = FakeDataBase.People.Select(p => int.Parse(p.Id)).Max();
                p.Id = lastId++.ToString();
            }

            if (p is InstructorDTO instructor)
            {
                FakeDataBase.People.Add(new Instructor(instructor));
            }
            else if( p is TeachingAssistantDTO assistant)
            {
                FakeDataBase.People.Add(new TeachingAssistant(assistant));
            }

            return p;
        }
    }
}
